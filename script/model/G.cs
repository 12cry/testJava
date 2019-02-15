using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using DG.Tweening;
using Newtonsoft.Json;
using testCC.Assets.script;
using testCC.Assets.script.model.card;
using testJava.script.constant;
using testJava.script.model.card;
using UnityEngine;
namespace testJava.script.model {
    public class G {
        public GCtrl ctrl;

        public Queue<CardCtrl> civilCardCtrls;
        public CardCtrl[] rowCardCtrls;
        public List<CardCtrl> handCardCtrls = new List<CardCtrl> ();
        public List<CardCtrl> passCardCtrls = new List<CardCtrl> ();

        List<Card> allCards = new List<Card> ();
        public GState state;

        string dir = "./Assets/data/";
        string cardDir = "./Assets/data/card/";
        public Dictionary<string, Dictionary<string, int>> conf;

        public void addCards<T> (string fileName) where T : Card {
            List<T> cards = JsonConvert.DeserializeObject<List<T>> (File.ReadAllText (cardDir + fileName + ".json", Encoding.UTF8));
            for (int i = 0; i < cards.Count; i++) {
                allCards.Add (cards[i]);
            }

        }
        public void initCards () {
            Type g = Type.GetType ("testJava.script.model.G");
            MethodInfo mi = g.GetMethod ("addCards");

            string ns = "testJava.script.model.card.";
            DirectoryInfo dirInfo = new DirectoryInfo (cardDir);
            FileInfo[] fis = dirInfo.GetFiles ("*.json");
            foreach (FileInfo fi in fis) {
                string cardName = fi.Name.Substring (0, fi.Name.IndexOf ("."));
                Type c = Type.GetType (ns + cardName);
                mi.MakeGenericMethod (new Type[] { c }).Invoke (this, new object[] { cardName });
            }
        }
        public void initConf () {
            conf = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>> (File.ReadAllText (dir + "conf.json", Encoding.UTF8));
        }
        public void init () {
            rowCardCtrls = new CardCtrl[ctrl.rowCardLimitNum];

            initCards ();
            initConf ();
        }
        public void play () {
            playInit ();
            deal ();
        }
        public void playInit () {
            List<Card> cards = new List<Card> ();
            for (int i = 0; i < allCards.Count; i++) {
                cards.Insert (UnityEngine.Random.Range (0, i + 1), allCards[i]);
            }

            civilCardCtrls = new Queue<CardCtrl> ();
            cards.ForEach (card => {
                CardCtrl newCtrdCtrl = UnityEngine.Object.Instantiate<CardCtrl> (U.ui.ctrl.cardCtrlPrefab, U.ui.ctrl.cardCtrlPrefab.transform.parent);
                newCtrdCtrl.card = card;
                card.ctrl = newCtrdCtrl;
                civilCardCtrls.Enqueue (newCtrdCtrl);
            });

            U.ui.ctrl.cardCtrlBackgroud.transform.SetAsLastSibling ();
        }
        public void deal () {
            computeCurrentCards ();
            showCurrentCards ();
        }
        CardCtrl getANewCard () {
            if (civilCardCtrls.Count == 0) {
                return null;
            }
            CardCtrl cardCtrl = civilCardCtrls.Dequeue ();
            cardCtrl.card.init ();
            return cardCtrl;
        }
        public void computeCurrentCards () {
            CardCtrl cardCtrl;

            int index = 0;

            for (int i = 0; i < ctrl.rowCardLimitNum; i++) {
                cardCtrl = rowCardCtrls[i];
                if (cardCtrl == null) {
                    continue;
                }
                rowCardCtrls[i] = null;
                if (cardCtrl.card.state != CardState.SHOWING) {
                    continue;
                }

                if (i < ctrl.removeCardNum) {
                    U.hideCard (cardCtrl);
                    continue;
                }

                rowCardCtrls[index++] = cardCtrl;
            }

            for (int i = index; i < ctrl.rowCardLimitNum; i++) {
                cardCtrl = getANewCard ();
                if (cardCtrl == null) {
                    state = GState.OVER;
                    break;
                }
                rowCardCtrls[i] = cardCtrl;
            }

        }
        public void showCurrentCards () {
            Tween tweener = null;
            CardCtrl cardCtrl = null;
            for (int i = 0; i < rowCardCtrls.Length; i++) {
                cardCtrl = rowCardCtrls[i];
                if (cardCtrl == null) {
                    break;
                }
                tweener = cardCtrl.transform.DOLocalMove (new Vector3 (U.cardWidth / 2 + i * U.cardWidth - Screen.width / 2, Screen.height / 2 - U.cardHeight / 2, 0), U.cardMoveSpeed);
                cardCtrl.card.takeCivil = 1 + i / 5;
                cardCtrl.card.showIndex = i;
                cardCtrl.card.show ();

            }
            if (tweener == null) {
                return;
            }
            tweener.OnComplete (() => onCompleteShow (cardCtrl));
        }
        void onCompleteShow (CardCtrl cardCtrl) {
            U.ui.ctrl.cardNumText.text = civilCardCtrls.Count.ToString ();
        }

    }
}
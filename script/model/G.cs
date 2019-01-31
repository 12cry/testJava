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

        public List<T> addCards<T> (string fileName) where T : Card {
            List<T> t = new List<T> ();
            string dir = "./Assets/data/";
            List<T> cards = JsonConvert.DeserializeObject<List<T>> (File.ReadAllText (dir + fileName + ".json", Encoding.UTF8));
            for (int i = 0; i < cards.Count; i++) {
                allCards.Add (cards[i]);
            }

            return t;
        }
        public void initCards () {
            Type g = Type.GetType ("testJava.script.model.G");
            MethodInfo mi = g.GetMethod ("addCards");

            string ns = "testJava.script.model.card.civil.";
            string path = "Assets/data";
            DirectoryInfo dir = new DirectoryInfo (path);
            FileInfo[] fis = dir.GetFiles ("*.json");
            foreach (FileInfo fi in fis) {
                string cardName = fi.Name.Substring (0, fi.Name.IndexOf ("."));
                Type c = Type.GetType (ns + cardName);
                mi.MakeGenericMethod (new Type[] { c }).Invoke (this, new object[] { cardName });
            }
        }
        public void init () {
            rowCardCtrls = new CardCtrl[ctrl.rowCardLimitNum];

            initCards ();
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
                if (cardCtrl.card.state == CardState.TAKED) {
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
            for (int i = 0; i < rowCardCtrls.Length; i++) {
                CardCtrl cardCtrl = rowCardCtrls[i];
                if (cardCtrl == null) {
                    break;
                }
                Tweener tweener = cardCtrl.transform.DOLocalMove (new Vector3 (U.cardWidth / 2 + i * U.cardWidth - Screen.width / 2, Screen.height / 2 - U.cardWidth / 2, 0), U.cardMoveSpeed);
                if (i == rowCardCtrls.Length - 1) {
                    tweener.OnComplete (() => onCompleteShow (cardCtrl));
                }
                cardCtrl.card.takeCivil = 1 + i / 5;
                cardCtrl.card.showIndex = i;
                cardCtrl.card.show ();

            }
        }
        void onCompleteShow (CardCtrl cardCtrl) {
            U.ui.ctrl.cardNumText.text = civilCardCtrls.Count.ToString ();
        }

    }
}
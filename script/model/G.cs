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
using UnityEngine.UI;

namespace testJava.script.model {
    public class G {
        public GCtrl ctrl;

        public Queue<CardCtrl> civilCardCtrls;
        public CardCtrl[] rowCardCtrls;
        public List<CardCtrl> handCardCtrls = new List<CardCtrl> ();
        public List<CardCtrl> passCardCtrls = new List<CardCtrl> ();

        List<Card> allCards = new List<Card> ();
        public GState state;

        public Dictionary<string, Dictionary<string, int>> conf;

        public void init () {

            rowCardCtrls = new CardCtrl[ctrl.rowCardLimitNum];

            initConf ();
            initCards ();
        }
        public void initCards () {
            Type g = Type.GetType ("testJava.script.model.G");
            MethodInfo mi = g.GetMethod ("addCards");
            string ns = "testJava.script.model.card.";

            // string[] cardNames = new string[] { "WonderCard", "GovernmentCard", "MilitaryBuildingCard" };
            string[] cardNames = new string[] { "WonderCard", "ResourceBuildingCard", "GovernmentCard", "MilitaryBuildingCard" };
            foreach (string cardName in cardNames) {
                Type c = Type.GetType (ns + cardName);

                mi.MakeGenericMethod (new Type[] { c }).Invoke (this, new object[] { cardName });
            }
        }
        public void initConf () {
            string text = U.LoadFile ("data/conf.json");
            conf = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>> (text);

            Config config = new Config ();
            U.config = config;
            foreach (var d in conf["config"]) {
                config.GetType ().GetProperty (d.Key).SetValue (config, d.Value, null);
            }

            CanvasScaler cs = ctrl.uICtrl.GetComponent<CanvasScaler> ();
            float screenWidth = cs.referenceResolution.x;
            float scale = Screen.width / screenWidth;
            config.scale = scale;
            config.cardWidth = config.srcCardWidth * scale;
            config.cardHeight = config.srcCardHeight * scale;
            config.cardWidthAndGap = (config.srcCardWidth + config.srcCardWidthGap) * scale;

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
            float statisticUIHeight = U.ui.statisticUI.ctrl.GetComponent<RectTransform> ().rect.height * U.config.scale;
            Vector2 v = new Vector2 (Screen.width - 100, Screen.height - statisticUIHeight / 2);
            Vector2 s = new Vector2 (statisticUIHeight / U.config.cardHeight, statisticUIHeight / U.config.cardHeight);
            civilCardCtrls = new Queue<CardCtrl> ();
            cards.ForEach (card => {
                CardCtrl newCtrdCtrl = UnityEngine.Object.Instantiate<CardCtrl> (U.ui.ctrl.cardCtrlPrefab, U.ui.ctrl.transform);
                newCtrdCtrl.transform.position = v;
                newCtrdCtrl.transform.localScale = s;
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
            var rect = ctrl.uICtrl.statisticUICtrl.GetComponent<RectTransform> ().rect;
            for (int i = 0; i < rowCardCtrls.Length; i++) {
                cardCtrl = rowCardCtrls[i];
                if (cardCtrl == null) {
                    break;
                }
                tweener = cardCtrl.transform.DOMove (new Vector3 (U.config.cardWidth / 2 + i * U.config.cardWidthAndGap,
                    Screen.height - U.config.cardHeight / 2 - rect.height * U.config.scale, 0), U.config.cardMoveSpeed);
                cardCtrl.transform.localScale = new Vector2 (1, 1);
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
        CardCtrl getANewCard () {
            if (civilCardCtrls.Count == 0) {
                return null;
            }
            CardCtrl cardCtrl = civilCardCtrls.Dequeue ();
            cardCtrl.card.init ();
            return cardCtrl;
        }

        public void addCards<T> (string fileName) where T : Card {
            string text = U.LoadFile ("data/card/" + fileName + ".json");
            List<T> cards = JsonConvert.DeserializeObject<List<T>> (text);
            for (int i = 0; i < cards.Count; i++) {
                allCards.Add (cards[i]);
            }

        }
    }
}
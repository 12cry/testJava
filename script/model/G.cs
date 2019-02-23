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

        List<InteriorCard> interiorCards = new List<InteriorCard> ();
        Queue<CardCtrl> interiorCardCtrls;
        CardCtrl[] rowCardCtrls;
        public List<CardCtrl> interiorHandCardCtrls = new List<CardCtrl> ();
        public List<CardCtrl> interiorPassCardCtrls = new List<CardCtrl> ();

        List<DiplomacyCard> diplomacyCards = new List<DiplomacyCard> ();
        Queue<CardCtrl> diplomacyCardCtrls;
        public List<CardCtrl> diplomacyHandCardCtrls = new List<CardCtrl> ();
        public List<CardCtrl> diplomacyPrepareCardCtrls = new List<CardCtrl> ();
        public List<CardCtrl> diplomacyPassCardCtrls = new List<CardCtrl> ();

        public GState state;

        public Dictionary<string, Dictionary<string, int>> conf;

        public void init () {
            initConf ();
            initCards ();

            rowCardCtrls = new CardCtrl[U.config.rowCardLimitNum];
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
        public void initCards () {
            Type g = Type.GetType ("testJava.script.model.G");
            string ns = "testJava.script.model.card.";

            MethodInfo mi = g.GetMethod ("addCards");
            string[] cardNames = new string[] { "WonderCard", "ResourceBuildingCard", "GovernmentCard", "MilitaryBuildingCard" };
            foreach (string cardName in cardNames) {
                Type c = Type.GetType (ns + cardName);
                mi.MakeGenericMethod (new Type[] { c }).Invoke (this, new object[] { cardName, interiorCards });
            }
            cardNames = new string[] { "CityCard" };
            foreach (string cardName in cardNames) {
                Type c = Type.GetType (ns + cardName);
                mi.MakeGenericMethod (new Type[] { c }).Invoke (this, new object[] { cardName, diplomacyCards });
            }
        }
        public void play () {
            playInit ();
            deal ();
        }
        public void playInit () {
            List<Card> cards = new List<Card> ();
            for (int i = 0; i < interiorCards.Count; i++) {
                cards.Insert (UnityEngine.Random.Range (0, i + 1), interiorCards[i]);
            }
            float statisticUIHeight = U.ui.statisticUI.ctrl.GetComponent<RectTransform> ().rect.height * U.config.scale;
            Vector2 v = new Vector2 (Screen.width - 100, Screen.height - statisticUIHeight / 2);
            Vector2 s = new Vector2 (statisticUIHeight / U.config.cardHeight, statisticUIHeight / U.config.cardHeight);
            interiorCardCtrls = new Queue<CardCtrl> ();
            cards.ForEach (card => {
                CardCtrl newCtrdCtrl = UnityEngine.Object.Instantiate<CardCtrl> (U.ui.ctrl.cardCtrlPrefab, U.ui.ctrl.transform);
                newCtrdCtrl.transform.position = v;
                newCtrdCtrl.transform.localScale = s;
                newCtrdCtrl.card = card;
                card.ctrl = newCtrdCtrl;
                interiorCardCtrls.Enqueue (newCtrdCtrl);
            });

            U.ui.ctrl.cardCtrlBackgroud.transform.SetAsLastSibling ();
        }
        public void deal () {
            computeRowCards ();
            showRowCards ();
        }

        public void computeRowCards () {
            CardCtrl cardCtrl;

            int index = 0;

            for (int i = 0; i < U.config.rowCardLimitNum; i++) {
                cardCtrl = rowCardCtrls[i];
                if (cardCtrl == null) {
                    continue;
                }
                rowCardCtrls[i] = null;
                if (cardCtrl.card.state != CardState.INROW) {
                    continue;
                }

                if (i < U.config.removeCardNum) {
                    U.hideCard (cardCtrl);
                    continue;
                }

                rowCardCtrls[index++] = cardCtrl;
            }

            for (int i = index; i < U.config.rowCardLimitNum; i++) {
                cardCtrl = getANewCard ();
                if (cardCtrl == null) {
                    state = GState.OVER;
                    break;
                }
                rowCardCtrls[i] = cardCtrl;
            }

        }
        public void showRowCards () {
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
                ((InteriorCard) cardCtrl.card).takeCivil = 1 + i / 5;
                cardCtrl.card.state = CardState.INROW;

            }
            if (tweener == null) {
                return;
            }
            tweener.OnComplete (() => onCompleteShow (cardCtrl));
        }
        void onCompleteShow (CardCtrl cardCtrl) {
            U.ui.ctrl.cardNumText.text = interiorCardCtrls.Count.ToString ();
        }
        CardCtrl getANewCard () {
            if (interiorCardCtrls.Count == 0) {
                return null;
            }
            CardCtrl cardCtrl = interiorCardCtrls.Dequeue ();
            cardCtrl.card.init ();
            return cardCtrl;
        }

        public void addCards<T> (string fileName, List<Card> addCards) where T : Card {
            string text = U.LoadFile ("data/card/" + fileName + ".json");
            List<T> cards = JsonConvert.DeserializeObject<List<T>> (text);
            for (int i = 0; i < cards.Count; i++) {
                addCards.Add (cards[i]);
            }

        }
    }
}
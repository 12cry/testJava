using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using DG.Tweening;
using Newtonsoft.Json;
using testCC.Assets.script;
using testJava.script.constant;
using testJava.script.model.card;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

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
        public List<CardCtrl> diplomacyPassCardCtrls = new List<CardCtrl> ();
        public List<DiplomacyCard> diplomacyPrepareCards = new List<DiplomacyCard> ();

        public GState state;
        public int leaderRountNum = 0;
        public Queue<int> playerIds;

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

            MethodInfo mi = g.GetMethod ("addInteriorCards");
            string[] cardNames = new string[] {
                "WonderCard",
                "ResourceBuildingCard",
                "GovernmentCard",
                "MilitaryBuildingCard",
                "LeaderCard"
            };
            foreach (string cardName in cardNames) {
                Type c = Type.GetType (ns + cardName);
                mi.MakeGenericMethod (new Type[] { c }).Invoke (this, new object[] { cardName });
            }
            cardNames = new string[] { "CityCard" };
            mi = g.GetMethod ("addDiplomacyCards");
            foreach (string cardName in cardNames) {
                Type c = Type.GetType (ns + cardName);
                mi.MakeGenericMethod (new Type[] { c }).Invoke (this, new object[] { cardName });
            }
        }
        public void play () {
            int playerNum = 2;
            playerIds = new Queue<int> ();
            playerIds.Enqueue (0);
            for (int i = 1; i < playerNum; i++) {
                playerIds.Enqueue (i);
                Object.Instantiate (U.ui.ctrl.playerUICtrl, U.ui.ctrl.transform);
            }

            initInteriorCards ();
            initDiplomacyCards ();
            roundInit ();
        }
        public void initInteriorCards () {
            List<Card> cards = new List<Card> ();
            for (int i = 0; i < interiorCards.Count; i++) {
                cards.Insert (UnityEngine.Random.Range (0, i + 1), interiorCards[i]);
            }
            float statisticUIHeight = U.ui.statisticUI.ctrl.GetComponent<RectTransform> ().rect.height * U.config.scale;
            Vector2 p = new Vector2 (Screen.width - 100, Screen.height - statisticUIHeight / 2);
            Vector2 s = new Vector2 (statisticUIHeight / U.config.cardHeight, statisticUIHeight / U.config.cardHeight);
            interiorCardCtrls = new Queue<CardCtrl> ();
            cards.ForEach (card => {
                CardCtrl newCtrdCtrl = UnityEngine.Object.Instantiate<CardCtrl> (U.ui.ctrl.cardCtrlPrefab, U.ui.ctrl.transform);
                newCtrdCtrl.transform.position = p;
                newCtrdCtrl.transform.localScale = s;
                newCtrdCtrl.card = card;
                card.ctrl = newCtrdCtrl;
                card.init ();
                if (card.isInit) {
                    card.initAction ();
                } else {
                    interiorCardCtrls.Enqueue (newCtrdCtrl);
                }
            });
            U.ui.ctrl.cardCtrlBackgroud.transform.SetAsLastSibling ();

        }
        public void initDiplomacyCards () {

            List<Card> cards = new List<Card> ();
            for (int i = 0; i < diplomacyCards.Count; i++) {
                cards.Insert (UnityEngine.Random.Range (0, i + 1), diplomacyCards[i]);
            }
            float statisticUIHeight = U.ui.statisticUI.ctrl.GetComponent<RectTransform> ().rect.height * U.config.scale;
            Vector2 p = new Vector2 (Screen.width - 50, Screen.height - statisticUIHeight / 2);
            Vector2 s = new Vector2 (statisticUIHeight / U.config.cardHeight, statisticUIHeight / U.config.cardHeight);
            diplomacyCardCtrls = new Queue<CardCtrl> ();
            cards.ForEach (card => {
                CardCtrl newCtrdCtrl = UnityEngine.Object.Instantiate<CardCtrl> (U.ui.ctrl.cardCtrlPrefab, U.ui.ctrl.transform);
                newCtrdCtrl.transform.position = p;
                newCtrdCtrl.transform.localScale = s;
                newCtrdCtrl.card = card;
                card.ctrl = newCtrdCtrl;
                diplomacyCardCtrls.Enqueue (newCtrdCtrl);
            });

        }
        void leaderHandle () {
            leaderRountNum++;
            WorkerBuildingCard card = (WorkerBuildingCard) U.ui.getBuildingCards (CardType.MILITARY_BUILDING).Find (c => c.id == CardId.WARRIOR);
            if (card == null) {
                return;
            }
            if (U.currentLeader == CardId.LEADER_MZD && leaderRountNum % 2 == 1) {
                card.buildCost = new Statistic ();
            } else {
                card.buildCost = card.buildCostBackup;
            }
        }
        public void roundInit () {
            leaderHandle ();
            U.ui.actionUI.reset ();
            U.ui.statisticUI.evaluating ();
            refreshCard ();

            deal ();
        }
        public void refreshCard () {
            foreach (CardCtrl cardCtrl in U.g.interiorHandCardCtrls) {
                Card card = cardCtrl.card;
                if (card is BonusCard) {
                    card.setActionAble (true);
                }
            }
        }

        public void deal () {
            dealInteriorCard ();
            dealDiplomacyCard ();
        }
        public void dealInteriorCard () {
            computeRowCards ();
            showRowCards ();
        }
        public void dealDiplomacyCard () {
            for (int i = 0; i < 2; i++) {
                if (diplomacyCardCtrls.Count == 0) {
                    break;
                }
                var cardCtrl = diplomacyCardCtrls.Dequeue ();
                U.g.diplomacyHandCardCtrls.Add (cardCtrl);
                cardCtrl.transform.DOMove (new Vector3 (U.config.cardWidth / 2 + U.g.interiorHandCardCtrls.Count * 20 + 200, U.config.cardHeight / 2, 0), U.config.cardMoveSpeed);
                cardCtrl.transform.DOScale (new Vector3 (1, 1, 0), U.config.cardMoveSpeed);
                cardCtrl.card.state = CardState.INHAND;
                cardCtrl.card.init ();
            }
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
            return cardCtrl;
        }

        public void addInteriorCards<T> (string fileName) where T : InteriorCard {
            string text = U.LoadFile ("data/card/" + fileName + ".json");
            List<T> cards = JsonConvert.DeserializeObject<List<T>> (text);
            for (int i = 0; i < cards.Count; i++) {
                interiorCards.Add (cards[i]);
            }

        }

        public void addDiplomacyCards<T> (string fileName) where T : DiplomacyCard {
            string text = U.LoadFile ("data/card/" + fileName + ".json");
            List<T> cards = JsonConvert.DeserializeObject<List<T>> (text);
            for (int i = 0; i < cards.Count; i++) {
                diplomacyCards.Add (cards[i]);
            }

        }
    }
}
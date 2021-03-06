using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using DG.Tweening;
using Newtonsoft.Json;
using testCC.Assets.script;
using testJava.script.constant;
using testJava.script.ctrl;
using testJava.script.ctrl.ui;
using testJava.script.model.card;
using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace testJava.script.model {
    public class G {
        public GCtrl ctrl;

        public CardCtrl[] rowCardCtrls;

        List<InteriorCard> interiorCards = new List<InteriorCard> ();
        Queue<CardCtrl> interiorCardCtrls;
        public List<CardCtrl> interiorPassCardCtrls = new List<CardCtrl> ();

        List<DiplomacyCard> diplomacyCards = new List<DiplomacyCard> ();
        Queue<CardCtrl> diplomacyCardCtrls;

        public List<CardCtrl> diplomacyPassCardCtrls = new List<CardCtrl> ();
        public List<DiplomacyCard> diplomacyPrepareCards = new List<DiplomacyCard> ();

        public GState state;
        public int leaderRountNum = 0;
        public Queue<int> playerIdQueue;
        public int[] playerIds;
        public Dictionary<int, PlayerUI> playerUIDic = new Dictionary<int, PlayerUI> ();
        public Dictionary<int, PlayerWorld> playerWorldDic = new Dictionary<int, PlayerWorld> ();
        public Dictionary<string, Dictionary<string, int>> conf;
        List<Card> isInitCards = new List<Card> ();

        AI ai = new AI ();
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
        public void nextPlayer () {
            playerIdQueue.Enqueue (U.cpId);
            int playerId = playerIdQueue.Dequeue ();
            U.cpId = playerId;

            switchPlayerUI (playerId);
            roundInit ();
        }
        public void switchPlayerUI (int playerId) {
            bool active = false;
            foreach (int id in playerIds) {
                if (playerId == id) {
                    active = true;
                } else {
                    active = false;
                }
                playerUIDic[id].ctrl.gameObject.SetActive (active);
                playerWorldDic[id].ctrl.gameObject.SetActive (active);
            }
            U.cpUI = playerUIDic[playerId];
            U.cpWorld = playerWorldDic[playerId];
        }
        public void play () {
            initInteriorCards ();
            initDiplomacyCards ();

            int playerNum = 2;
            playerIds = new int[playerNum];
            playerIdQueue = new Queue<int> ();
            for (int i = 1; i < playerNum; i++) {
                playerIds[i] = i;
                playerIdQueue.Enqueue (i);
                PlayerUICtrl playerUICtrl = Object.Instantiate (U.ui.ctrl.playerUICtrl, U.ui.ctrl.transform);
                playerUICtrl.init ();
                U.cpUI = playerUICtrl.ui;
                playerUIDic.Add (i, U.cpUI);

                PlayerWorldCtrl playerWorldCtrl = Object.Instantiate (U.world.playerWorld.ctrl, U.world.ctrl.transform);
                playerWorldCtrl.init ();
                playerWorldCtrl.name = "AI-" + i;
                U.cpWorld = playerWorldCtrl.world;
                playerWorldDic.Add (i, U.cpWorld);

                foreach (var card in isInitCards) {
                    card.initAction ();
                }

                U.ui.orgUI.addAPlayer (i);
            }
            playerIds[0] = 0;
            U.cpId = 0;
            U.cpUI = U.ui.playerUI;
            playerUIDic.Add (0, U.cpUI);
            U.cpWorld = U.world.playerWorld;
            playerWorldDic.Add (0, U.cpWorld);
            U.ui.orgUI.addAPlayer (0);
            foreach (var card in isInitCards) {
                card.initAction ();
            }

            switchPlayerUI (0);
            roundInit ();
        }
        public void initInteriorCards () {
            List<Card> cards = new List<Card> ();
            for (int i = 0; i < interiorCards.Count; i++) {
                cards.Insert (UnityEngine.Random.Range (0, i + 1), interiorCards[i]);
            }
            float statisticUIHeight = U.ui.playerUI.statisticUI.ctrl.GetComponent<RectTransform> ().rect.height * U.config.scale;
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
                    isInitCards.Add (card);
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
            float statisticUIHeight = U.ui.playerUI.statisticUI.ctrl.GetComponent<RectTransform> ().rect.height * U.config.scale;
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
            WorkerBuildingCard card = (WorkerBuildingCard) U.cpUI.getBuildingCards (CardType.MILITARY_BUILDING).Find (c => c.id == CardId.WARRIOR);
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
            U.cpUI.actionUI.reset ();
            U.cpUI.statisticUI.evaluating ();
            refreshCard ();

            deal ();
        }
        public void refreshCard () {
            foreach (CardCtrl cardCtrl in U.cpUI.handCardUI.interiorHandCardCtrls) {
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
                U.cpUI.handCardUI.diplomacyHandCardCtrls.Add (cardCtrl);
                cardCtrl.transform.DOMove (new Vector3 (U.config.cardWidth / 2 + U.cpUI.handCardUI.interiorHandCardCtrls.Count * 20 + 200, U.config.cardHeight / 2, 0), U.config.cardMoveSpeed);
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
            var rect = U.cpUI.statisticUI.ctrl.GetComponent<RectTransform> ().rect;
            for (int i = 0; i < rowCardCtrls.Length; i++) {
                cardCtrl = rowCardCtrls[i];
                if (cardCtrl == null) {
                    break;
                }
                tweener = cardCtrl.transform.DOMove (new Vector3 (U.config.cardWidth / 2 + i * U.config.cardWidthAndGap,
                    Screen.height - U.config.cardHeight / 2 - rect.height * U.config.scale, 0), U.config.cardMoveSpeed);
                cardCtrl.transform.localScale = new Vector2 (1, 1);
                ((InteriorCard) cardCtrl.card).takeInterior = 1 + i / 5;
                cardCtrl.card.state = CardState.INROW;

            }
            if (tweener == null) {
                return;
            }
            tweener.OnComplete (() => onCompleteShow (cardCtrl));
        }
        void onCompleteShow (CardCtrl cardCtrl) {
            U.ui.ctrl.cardNumText.text = interiorCardCtrls.Count.ToString ();
            if (U.cpId != 0) {
                ai.run ();
            }
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
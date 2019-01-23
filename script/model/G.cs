using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DG.Tweening;
using Newtonsoft.Json;
using testCC.Assets.script;
using testCC.Assets.script.ctrl;
using testCC.Assets.script.model.card;
using testJava.script.constant;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.model {
    public class G {
        public GCtrl ctrl;

        public Queue<CardCtrl> civilCardCtrls;
        public CardCtrl[] rowCardCtrls;
        public List<CardCtrl> handCardCtrls = new List<CardCtrl> ();
        public List<CardCtrl> passCardCtrls = new List<CardCtrl> ();
        int rowCardLimitNum = 3;
        public bool over = false;
        public void init () {
            string json = File.ReadAllText ("./Assets/data/cardBuild.json", Encoding.UTF8);
            List<ResourceBuildingCard> cardBuildList = JsonConvert.DeserializeObject<List<ResourceBuildingCard>> (json);
            List<CardLeader> cardLeaderList = JsonConvert.DeserializeObject<List<CardLeader>> (File.ReadAllText ("./Assets/data/cardLeader.json", Encoding.UTF8));

            List<Card> allCardList = new List<Card> ();
            for (int i = 0; i < cardBuildList.Count; i++) {
                allCardList.Add (cardBuildList[i]);
            }
            for (int i = 0; i < cardLeaderList.Count; i++) {
                allCardList.Add (cardLeaderList[i]);
            }

            List<Card> cardList = new List<Card> ();
            for (int i = 0; i < allCardList.Count; i++) {
                cardList.Insert (Random.Range (0, i + 1), allCardList[i]);
            }

            civilCardCtrls = new Queue<CardCtrl> ();
            cardList.ForEach (card => {
                CardCtrl newCtrdCtrl = Object.Instantiate<CardCtrl> (ctrl.cardCtrlPrefab, ctrl.cardCtrlPrefab.transform.parent);
                newCtrdCtrl.card = card;
                card.ctrl = newCtrdCtrl;
                civilCardCtrls.Enqueue (newCtrdCtrl);
            });

            rowCardCtrls = new CardCtrl[rowCardLimitNum];
        }
        public void deal () {
            computeCurrentCards ();
            showCurrentCards ();
        }
        // public void removeARowCardCtrl (CardCtrl cardCtrl) {
        //     for (int i = 0; i < rowCardCtrls.Length; i++) {
        //         if (rowCardCtrls[i] == cardCtrl) {
        //             rowCardCtrls[i] = null;
        //             break;
        //         }
        //     }
        // }
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
            int removeCardNum = 1;
            int index = 0;

            for (int i = 0; i < rowCardLimitNum; i++) {
                cardCtrl = rowCardCtrls[i];
                if (cardCtrl == null) {
                    continue;
                }
                rowCardCtrls[i] = null;
                if (cardCtrl.card.state == CardState.TAKED) {
                    continue;
                }

                if (i < removeCardNum) {
                    U.hideCard (cardCtrl);
                    continue;
                }

                rowCardCtrls[index++] = cardCtrl;
            }

            for (int i = index; i < rowCardLimitNum; i++) {
                cardCtrl = getANewCard ();
                if (cardCtrl == null) {
                    over = true;
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
                cardCtrl.transform.DOLocalMove (new Vector3 (U.cardWidth / 2 + i * U.cardWidth - Screen.width / 2, Screen.height / 2 - U.cardWidth / 2, 0), U.cardMoveSpeed);
                // .OnComplete (() => onCompleteShow (cardCtrl));
                cardCtrl.card.takeCivil = 1 + i / 5;
                cardCtrl.card.showIndex = i;
                cardCtrl.card.show ();

            }
        }
        // void onCompleteShow (CardCtrl cardCtrl) {
        //     cardCtrl.card.showingPosition = cardCtrl.transform.localPosition;
        // }

    }
}
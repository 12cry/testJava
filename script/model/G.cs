using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DG.Tweening;
using Newtonsoft.Json;
using testCC.Assets.script;
using testCC.Assets.script.ctrl;
using testJava.script.constant;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.model {
    public class G {
        public GCtrl ctrl;

        public Queue<CardCtrl> civilCardCtrls;
        public CardCtrl[] rowCardCtrls;
        int rowCardLimitNum = 3;
        public bool over = false;
        public void init () {
            string json = File.ReadAllText ("./Assets/testJava/Resources/cardBuild.json", Encoding.UTF8);
            List<CardBuild> cardBuildList = JsonConvert.DeserializeObject<List<CardBuild>> (json);

            List<Card> cardList = new List<Card> ();
            for (int i = 0; i < cardBuildList.Count; i++) {
                cardList.Insert (Random.Range (i, i + 1), cardBuildList[i]);
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
                    cardCtrl.card.hideCard ();
                    continue;
                }

                rowCardCtrls[index] = cardCtrl;
                index++;
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
                if (rowCardCtrls[i] == null) {
                    break;
                }
                rowCardCtrls[i].transform.DOMove (new Vector3 (Utils.cardWidth / 2 + i * Utils.cardWidth, Screen.height - Utils.cardWidth / 2, 0), Utils.cardMoveSpeed);

                rowCardCtrls[i].card.takeCiv = 1 + i / 5;
                rowCardCtrls[i].card.state = CardState.SHOWING;
            }
        }

    }
}
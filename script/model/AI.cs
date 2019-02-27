using testCC.Assets.script;
using testJava.script.constant;
using UnityEngine;

namespace testJava.script.model {
    public class AI {
        public void run () {
            actionInteriorCard ();
            Debug.Log (U.cpUI.actionUI.interiorRemainder);
            int n = U.cpUI.actionUI.interiorRemainder;
            for (; U.cpUI.actionUI.interiorRemainder > 0;) {
                takeCard ();
            }
        }

        void actionInteriorCard () {

            foreach (CardCtrl cardCtrl in U.cpUI.handCardUI.interiorHandCardCtrls) {
                if (U.cpUI.actionUI.interiorRemainder == 0) {
                    return;
                }
                Card card = cardCtrl.card;
                if (!card.getActionAble ()) {
                    continue;
                }
                card.action ();
            }
        }
        void actionDiplomacyCard () { }
        void build () { }
        void addWorker () { }
        void takeCard () {
            if (isAllNull (0, 14)) {
                return;
            }
            if (isAllNull (0, 5)) {
                getFirstCard ().take ();
            }
            getRandomCard ().take ();
        }
        InteriorCard getRandomCard () {
            Card card = U.g.rowCardCtrls[getTakeCardIndex ()].card;
            if (card.state == CardState.INROW) {
                return (InteriorCard) card;
            } else {
                return getRandomCard ();
            }
        }
        InteriorCard getFirstCard () {
            for (int i = 0; i < 14; i++) {
                InteriorCard card = (InteriorCard) U.g.rowCardCtrls[i].card;
                if (card.state == CardState.INROW) {
                    return card;
                }
            }
            return null;
        }
        bool isAllNull (int a, int b) {
            for (int i = 0; i < b - a; i++) {
                if (U.g.rowCardCtrls[a + i].card.state == CardState.INROW) {
                    return false;
                }
            }
            return true;
        }
        int getTakeCardIndex () {
            int r = Random.Range (1, 100);
            if (r < 80) {
                return Random.Range (1, 5);
            } else if (r < 99) {
                return Random.Range (5, 10);
            } else {
                return Random.Range (10, 14);
            }
        }
    }
}
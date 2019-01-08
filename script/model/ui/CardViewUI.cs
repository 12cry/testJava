using testCC.Assets.script;
using testJava.script.ctrl.ui;
using UnityEngine;

namespace testJava.script.model.ui {
    public class CardViewUI {
        public CardViewUICtrl ctrl;

        public void showView () {
            ctrl.gameObject.SetActive (true);
            Utils.ui.mask (new Transform[] { Utils.currentCard.ctrl.transform, ctrl.transform });
        }
        public void hideView () {
            ctrl.gameObject.SetActive (false);
            Utils.ui.unMask ();
        }

        public void buttonAble (bool takeable, bool actionable) {
            ctrl.bTakeCard.gameObject.SetActive (takeable);
            ctrl.bActionCard.gameObject.SetActive (actionable);

        }

    }
}
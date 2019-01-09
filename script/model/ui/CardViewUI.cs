using testCC.Assets.script;
using testJava.script.ctrl.ui;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace testJava.script.model.ui {
    public class CardViewUI {
        public CardViewUICtrl ctrl;

        public void showView () {
            Card card = Utils.currentCard;
            ctrl.desc.text = card.desc;
            int index = 0;
            if (!card.taked) {
                Button button = ctrl.bTakeCard;
                button.transform.localPosition = new Vector3 (0, 30 * index++, 0);
                button.GetComponentInChildren<Text> ().text = "taked";
                button.onClick.AddListener (Utils.currentCard.take);

            } else if (card.taked && !card.actioned) {

            } else if (card.actioned) {

            }

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
using testCC.Assets.script;
using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.ctrl.ui {
    public class CardViewUICtrl : MonoBehaviour {
        public CardViewUI ui = new CardViewUI ();

        public Text desc;
        public Button bTakeCard;
        public Button bActionCard;

        void Start () {
            Debug.Log ("cardView");
            ui.ctrl = this;
            ui.hideView ();
        }

        public void takeCard () {
            Utils.currentCard.take ();
        }
        public void actionCard () {
            Utils.currentCard.action ();
        }
        public void closeCard () {
            Utils.currentCard.closeView ();
        }
    }
}
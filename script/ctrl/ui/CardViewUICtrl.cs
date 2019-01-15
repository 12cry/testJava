using System.Collections.Generic;
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
        public List<Button> buttons;
        public Image maskCardImage;
        public Image maskBuildingImage;

        void Start () {
            Debug.Log ("cardView");
            ui.ctrl = this;
            ui.closeView ();
        }

        public void takeCard () {
            Utils.currentCard.take ();
            ui.closeView ();
        }
        public void actionCard () {
            Utils.currentCard.action ();
            ui.closeView ();
        }
        public void closeCard () {
            Utils.currentCard.closeView ();
            ui.closeView ();
        }
        public void closeViewBuilding () {
            ui.closeViewBuilding ();
        }
        public void viewBuilding () {
            ui.viewBuilding ();
        }
    }
}
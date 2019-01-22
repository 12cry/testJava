using System.Collections.Generic;
using testCC.Assets.script;
using testJava.script.command;
using testJava.script.constant;
using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.ctrl.ui {
    public class CardViewUICtrl : MonoBehaviour {
        public CardViewUI ui;

        public Text desc;
        public Button bTakeCard;
        public Button bActionCard;
        public List<Button> buttons;
        public Image maskCardImage;
        public Image maskBuildingImage;

        public void init () {
            ui = new CardViewUI ();
            ui.ctrl = this;
            ui.init ();
        }

        public void takeCard () {
            U.currentCard.take ();
            ui.closeView ();

            CommandCtrl.instant.addCommand (new CardTakeCommand (U.currentCard));
        }
        public void actionCard () {
            U.currentCard.action ();
            ui.closeView ();

            CommandCtrl.instant.addCommand (new CardActionCommand (U.currentCard));
        }
        public void closeCard () {
            U.currentCard.closeView ();
            ui.closeView ();
        }
        public void closeViewBuilding () {
            ui.closeViewBuilding ();
        }
        public void viewBuilding (CardType cardType) {
            ui.viewBuilding (cardType);
        }
    }
}
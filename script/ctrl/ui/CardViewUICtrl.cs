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
        public Button buttonParfab;

        public void init () {
            ui = new CardViewUI ();
            ui.ctrl = this;
            ui.init ();
        }
        public void actionCard () {
            U.currentCard.action ();
            ui.closeView ();

            CommandCtrl.instant.addCommand (new CardActionCommand (U.currentCard));
        }
        public void closeViewCard () {
            U.currentCard.resetPosition ();
            ui.closeView ();
        }
    }
}
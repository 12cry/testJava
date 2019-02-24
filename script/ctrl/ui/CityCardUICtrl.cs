using System.Collections.Generic;
using testCC.Assets.script;
using testJava.script.command;
using testJava.script.constant;
using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.ctrl.ui {
    public class CityCardUICtrl : MonoBehaviour {
        public CityCardUI ui;

        public void init () {
            ui = new CityCardUI ();
            ui.ctrl = this;

            gameObject.SetActive (false);

        }
        public void close () {
            gameObject.SetActive (false);
            U.hideCard (U.currentCard.ctrl);
        }
        public void pass () {
            close ();
            U.currentCard.state = CardState.END;
        }
        public void bid () {
            close ();
            U.currentCard.state = CardState.END;
        }

    }
}
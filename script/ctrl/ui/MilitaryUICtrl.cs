using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.ctrl.ui {
    public class MilitaryUICtrl : MonoBehaviour {
        public MilitaryUI ui;
        public Text attackText;
        public Text defenseText;

        public void init () {
            ui = new MilitaryUI ();
            ui.ctrl = this;
        }

    }
}
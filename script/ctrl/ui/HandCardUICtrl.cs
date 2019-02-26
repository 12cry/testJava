using testJava.script.model.ui;
using UnityEngine;

namespace testJava.script.ctrl.ui {

    public class HandCardUICtrl : MonoBehaviour {
        public HandCardUI ui;
        public void init () {
            ui = new HandCardUI ();
            ui.ctrl = this;
        }
    }

}
using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.ctrl.ui {
    public class LeaderUICtrl : MonoBehaviour {
        public LeaderUI ui;
        public RawImage image;

        public void init () {
            ui = new LeaderUI ();
            ui.ctrl = this;
        }

    }
}
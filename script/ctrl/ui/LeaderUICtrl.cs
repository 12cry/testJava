using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.ctrl.ui {
    public class LeaderUICtrl : MonoBehaviour {
        public LeaderUI ui;
        public RawImage image;

        void Start () {
            ui = new LeaderUI ();
            ui.ctrl = this;
        }

    }
}
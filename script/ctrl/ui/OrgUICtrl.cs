using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.ctrl.ui {
    public class OrgUICtrl : MonoBehaviour {
        public OrgUI ui;

        public Image leaderImage;
        public RawImage governmentImage;

        public void init () {
            ui = new OrgUI ();
            ui.ctrl = this;
        }

    }
}
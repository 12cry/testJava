using testCC.Assets.script;
using testJava.script.model;
using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.ctrl.ui {
    public class ActionUICtrl : MonoBehaviour {
        public ActionUI ui = new ActionUI ();

        public Text civilText;
        public Text civilRemainderText;

        void Start () {
            ui.ctrl = this;
            ui.init ();
        }

        public void pass () {
            G g = Utils.g;
            if (g.over) {
                return;
            }

            g.deal ();

            ui.reset ();

            Utils.ui.resourceUI.evaluating ();

        }
    }
}
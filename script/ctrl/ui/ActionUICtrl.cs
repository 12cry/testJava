using testCC.Assets.script;
using testJava.script.command;
using testJava.script.model;
using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.ctrl.ui {
    public class ActionUICtrl : MonoBehaviour {
        public ActionUI ui;

        public Text civilText;
        public Text civilRemainderText;

        public Button undoButton;
        public Button redoButton;

        public void undo () {
            CommandCtrl.instant.undoCommands.Pop ().undo ();
        }
        public void redo () {
            CommandCtrl.instant.redoCommands.Pop ().redo ();
        }
        public void init () {
            ui = new ActionUI ();
            ui.ctrl = this;
            ui.init ();
        }

        public void pass () {
            G g = U.g;
            if (g.over) {
                return;
            }

            g.deal ();

            ui.reset ();

            U.ui.resourceUI.evaluating ();

        }
    }
}
using System;
using testCC.Assets.script;
using testJava.script.command;
using testJava.script.constant;
using testJava.script.model;
using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.ctrl.ui {
    public class ActionUICtrl : MonoBehaviour {
        public ActionUI ui;

        public Text interiorText;
        public Text interiorRemainderText;

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
            Debug.Log ("pass---");
            if (U.g.state == GState.OVER) {
                return;
            }
            U.g.nextPlayer ();
            // U.g.roundInit ();

        }
    }
}
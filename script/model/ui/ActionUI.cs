using testCC.Assets.script;
using testJava.script.command;
using testJava.script.ctrl.ui;
using testJava.script.model.card;

namespace testJava.script.model.ui {
    public class ActionUI {
        public ActionUICtrl ctrl;

        public int interior;
        public int interiorRemainder;

        public int military;
        public int militaryRemainder;

        public void init () {
            updateInteriorRemainder (8);
            setInterior (8);
        }
        public void setUndoButtonAble (bool able) {
            // ctrl.undoButton.interactable = able;
        }
        public void setRedoButtonAble (bool able) {
            // ctrl.redoButton.interactable = able;
        }
        public void reset () {
            this.interiorRemainder = this.interior;
            ctrl.interiorRemainderText.text = this.interiorRemainder.ToString ();

            CommandCtrl.instant.redoCommands.Clear ();
            CommandCtrl.instant.undoCommands.Clear ();
            // setUndoButtonAble (false);
            // setRedoButtonAble (false);
        }
        public void updateInteriorRemainder (int value) {
            this.interiorRemainder += value;
            ctrl.interiorRemainderText.text = this.interiorRemainder.ToString ();
        }
        public void updateDiplomacyRemainder (int value) { }
        public void setInterior (int value) {
            this.interior += value;
            ctrl.interiorText.text = this.interior.ToString ();
        }

    }
}
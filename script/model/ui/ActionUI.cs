using testJava.script.command;
using testJava.script.ctrl.ui;

namespace testJava.script.model.ui {
    public class ActionUI {
        public ActionUICtrl ctrl;

        public int civil;
        public int civilRemainder;

        public int military;
        public int militaryRemainder;

        public void init () {
            updateCivilRemainder (8);
            setCivil (8);
        }
        public void setUndoButtonAble (bool able) {
            ctrl.undoButton.interactable = able;
        }
        public void setRedoButtonAble (bool able) {
            ctrl.redoButton.interactable = able;
        }
        public void reset () {
            this.civilRemainder = this.civil;
            ctrl.civilRemainderText.text = this.civilRemainder.ToString ();

            CommandCtrl.instant.redoCommands.Clear ();
            CommandCtrl.instant.undoCommands.Clear ();
            setUndoButtonAble (false);
            setRedoButtonAble (false);
        }
        public void updateCivilRemainder (int value) {
            this.civilRemainder += value;
            ctrl.civilRemainderText.text = this.civilRemainder.ToString ();
        }
        public void setCivil (int value) {
            this.civil += value;
            ctrl.civilText.text = this.civil.ToString ();
        }

    }
}
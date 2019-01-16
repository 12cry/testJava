using testJava.script.ctrl.ui;

namespace testJava.script.model.ui {
    public class ActionUI {
        public ActionUICtrl ctrl;

        public int civil;
        public int civilRemainder;

        public void init () {
            updateCivilRemainder (4);
            updateCivil (4);
        }

        public void updateCivilRemainder (int value) {
            this.civilRemainder += value;
            ctrl.civilRemainderText.text = this.civilRemainder.ToString ();
        }
        public void updateCivil (int value) {
            this.civil += value;
            ctrl.civilText.text = this.civil.ToString ();
        }
    }
}
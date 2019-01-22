using testJava.script.ctrl.ui;

namespace testJava.script.model.ui {
    public class MilitaryUI {
        public MilitaryUICtrl ctrl;

        public int strength;

        public void updateStrength (int value) {
            this.strength += value;
            ctrl.strengthText.text = this.strength.ToString ();
        }
    }
}
using testJava.script.ctrl.ui;

namespace testJava.script.model.ui {
    public class MilitaryUI {
        public MilitaryUICtrl ctrl;

        public int attack;

        public void updateAttack (int value) {
            this.attack += value;
            ctrl.attackText.text = this.attack.ToString ();
        }
    }
}
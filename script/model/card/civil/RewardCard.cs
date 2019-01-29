using testJava.script.model;

namespace testCC.Assets.script.model.card {
    public class RewardCard : Card {
        public Statistic actionIncome;
        public bool _actionAble = false;

        public override void action () {
            U.ui.reduce (actionIncome);
            base.action ();
        }
        public override bool actionAble () {
            return _actionAble;
        }

    }
}
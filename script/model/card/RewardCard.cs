namespace testCC.Assets.script.model.card {
    public class RewardCard : Card {
        public Income actionIncome;
        public bool _actionAble = false;

        public override void action () {
            U.ui.resourceUI.addIncome (actionIncome);
            base.action ();
        }
        public override bool actionAble () {
            return _actionAble;
        }

    }
}
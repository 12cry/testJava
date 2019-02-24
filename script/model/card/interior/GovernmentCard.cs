using testCC.Assets.script;

namespace testJava.script.model.card {
    public class GovernmentCard : InteriorCard {
        public int civil;
        public int military;
        public Statistic actionCost;
        public Statistic actionIncome;
        public string iconPath;

        public override void action () {
            U.ui.actionUI.setCivil (civil);
            U.ui.actionUI.updateCivilRemainder (civil > U.ui.actionUI.civil?(civil - U.ui.actionUI.civil) : 0);
            U.ui.statisticUI.add (actionIncome);
            U.ui.orgUI.setGovernment (iconPath);
            U.ui.statisticUI.reduce (actionCost);
            base.action ();
        }
        public override void render () {
            base.render ();
            U.setActionCostText (ctrl, actionCost);
        }

    }
}
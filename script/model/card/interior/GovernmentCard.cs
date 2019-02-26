using testCC.Assets.script;

namespace testJava.script.model.card {
    public class GovernmentCard : InteriorCard {
        public int civil;
        public int military;
        public Statistic actionCost;
        public Statistic actionIncome;
        public string iconPath;

        public override void action () {
            U.cpUI.actionUI.setCivil (civil);
            U.cpUI.actionUI.updateCivilRemainder (civil > U.cpUI.actionUI.civil?(civil - U.cpUI.actionUI.civil) : 0);
            U.cpUI.statisticUI.add (actionIncome);
            U.ui.orgUI.setGovernment (iconPath);
            U.cpUI.statisticUI.reduce (actionCost);
            base.action ();
        }
        public override void render () {
            base.render ();
            U.setActionCostText (ctrl, actionCost);
        }

    }
}
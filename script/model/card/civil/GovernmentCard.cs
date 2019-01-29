using testCC.Assets.script;

namespace testJava.script.model.card {
    public class GovernmentCard : BuildingCard {
        public int civil;
        public int military;

        public Statistic actionIncome;
        public override void action () {
            U.ui.actionUI.setCivil (civil);
            U.ui.actionUI.updateCivilRemainder (civil > U.ui.actionUI.civil?(civil - U.ui.actionUI.civil) : 0);
            U.ui.add (actionIncome);
        }
    }
}
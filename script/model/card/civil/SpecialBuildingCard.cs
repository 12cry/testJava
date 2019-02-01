using testCC.Assets.script;
using testCC.Assets.script.model;

namespace testJava.script.model.card {
    public class SpecialBuildingCard : BuildingCard {
        public Statistic actionIncome;
        public override void action () {
            U.ui.add (actionIncome);
            base.action ();
        }

    }
}
using testCC.Assets.script;
using testCC.Assets.script.model;

namespace testJava.script.model.card {
    public class SpecialBuildingCard : BuildingCard {
        public Income actionIncome;
        public override void action () {
            U.ui.resourceUI.addIncome (actionIncome);
            base.action ();
        }
    }
}
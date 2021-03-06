using testCC.Assets.script;
using testJava.script.model;

namespace testJava.script.model.card {
    public class BonusCard : InteriorCard {
        public Statistic actionIncome;

        public override void init () {
            base.init ();
            setActionAble (false);
        }
        public override void action () {
            U.cpUI.statisticUI.add (actionIncome);
            base.action ();
        }
        public override void render () {
            base.render ();
            U.setActionIncomeText (ctrl, actionIncome);
        }

    }
}
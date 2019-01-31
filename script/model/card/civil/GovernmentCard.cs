using testCC.Assets.script;

namespace testJava.script.model.card.civil {
    public class GovernmentCard : Card {
        public int civil;
        public int military;
        public Statistic actionCost;
        public Statistic actionIncome;
        public string iconPath;

        public override void action () {
            U.ui.actionUI.setCivil (civil);
            U.ui.actionUI.updateCivilRemainder (civil > U.ui.actionUI.civil?(civil - U.ui.actionUI.civil) : 0);
            U.ui.add (actionIncome);
            U.ui.orgUI.setgovernment (iconPath);
            base.action ();
        }
        public override void render () {
            base.render ();
            if (actionCost != null) {
                textDic["costScience"].text = actionCost.science.ToString ();
            }
        }

    }
}
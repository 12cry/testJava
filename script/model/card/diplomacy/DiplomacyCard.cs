using testCC.Assets.script;
using testJava.script.model;

namespace testJava.script.model.card {
    public class DiplomacyCard : Card {

        public override void action () {
            U.g.diplomacyHandCardCtrls.Add (this.ctrl);
        }
    }
}
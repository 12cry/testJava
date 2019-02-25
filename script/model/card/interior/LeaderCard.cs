using testCC.Assets.script;

namespace testJava.script.model.card {
    public class LeaderCard : InteriorCard {
        public string leaderImage;
        public override void action () {
            U.ui.orgUI.appoint (this);
            U.g.leaderRountNum = 0;
            base.action ();
        }
    }
}
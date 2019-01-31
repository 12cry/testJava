namespace testCC.Assets.script.model.card {
    public class LeaderCard : Card {
        public string leaderImage;
        public override void action () {
            U.ui.orgUI.appoint (this);
            base.action ();
        }
    }
}
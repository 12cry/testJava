namespace testCC.Assets.script.model.card {
    public class CardLeader : Card {
        public string leaderImage;
        public override void action () {
            U.ui.leaderUI.appoint (this);
            base.action ();
        }
    }
}
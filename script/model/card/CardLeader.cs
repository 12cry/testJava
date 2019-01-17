namespace testCC.Assets.script.model.card {
    public class CardLeader : Card {
        public string leaderImage;
        public override void action () {
            Utils.ui.leaderUI.appoint (this);
        }
    }
}
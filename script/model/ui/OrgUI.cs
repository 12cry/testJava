using testCC.Assets.script;
using testJava.script.ctrl.ui;
using testJava.script.model.card;
using UnityEngine;

namespace testJava.script.model.ui {
    public class OrgUI {
        public OrgUICtrl ctrl;

        public void setGovernment (string iconPath) {
            Debug.Log ("setgovernment---");
        }
        public void appoint (LeaderCard card) {
            U.currentLeader = card.id;
            var s = Resources.Load<Sprite> (card.leaderImage);
            ctrl.leaderImage.sprite = s;
        }
    }
}
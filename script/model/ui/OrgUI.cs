using testCC.Assets.script;
using testJava.script.ctrl.ui;
using testJava.script.model.card;
using UnityEngine;
using UnityEngine.UI;

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
        public void addAPlayer (int playerId) {
            Image leaderMask = Object.Instantiate (ctrl.leaderMask, ctrl.transform);

            leaderMask.transform.position = new Vector2 (leaderMask.transform.position.x, leaderMask.transform.position.y - 100);
        }
    }
}
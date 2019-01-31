using testCC.Assets.script;
using testCC.Assets.script.model.card;
using testJava.script.ctrl.ui;
using UnityEngine;

namespace testJava.script.model.ui {
    public class OrgUI {
        public OrgUICtrl ctrl;

        public void setgovernment (string iconPath) {
            Debug.Log ("setgovernment---");
            // ctrl.governmentImage
        }
        public void appoint (LeaderCard card) {
            U.currentLeader = card.id;
        }
    }
}
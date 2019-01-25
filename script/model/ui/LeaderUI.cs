using testCC.Assets.script;
using testCC.Assets.script.model.card;
using testJava.script.ctrl.ui;
using UnityEngine;

namespace testJava.script.model.ui {
    public class LeaderUI {
        public LeaderUICtrl ctrl;

        public void appoint (LeaderCard card) {
            U.currentLeader = card.id;
        }
    }
}
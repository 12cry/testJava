using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.model;
using testJava.script.model;
using UnityEngine;

namespace testCC.Assets.script {
    public class Utils {
        public static float cardMoveSpeed = 0.1f;
        public static int cardWidth = 100;

        // public static Resource resource;
        public static G g;
        public static World world;
        public static UI ui;
        public static Card currentCard;
        public static int currentLeader;

        public static void hideCard (CardCtrl ctrl) {
            ctrl.gameObject.transform.localPosition = new Vector3 (-1000, 0, 0);
        }
    }
}
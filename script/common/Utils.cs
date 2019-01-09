using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.model;
using testJava.script.model;

namespace testCC.Assets.script {
    public class Utils {
        public static float cardMoveSpeed = 0.1f;
        public static int cardWidth = 100;

        // public static Resource resource;
        public static World world;
        public static UI ui;
        public static Card currentCard;

        public static List<CardCtrl> handCardCtrls = new List<CardCtrl> ();
        public static List<CardCtrl> passCardCtrls = new List<CardCtrl> ();

    }
}
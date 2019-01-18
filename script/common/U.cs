using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.model;
using testJava.script.model;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace testCC.Assets.script {
    public class U {
        public static float cardMoveSpeed = 0.1f;
        public static int cardWidth = 100;

        public static G g;
        public static World world;
        public static UI ui;
        public static Card currentCard;
        public static int currentLeader;

        public static void hideCard (CardCtrl ctrl) {
            ctrl.gameObject.transform.localPosition = new Vector3 (-1000, 0, 0);
        }
        public static void showAButton (Button b, int index) {
            b.transform.localPosition = new Vector3 (0, 30 * index, 0);
        }
        public static void addAButton (int index, string text, UnityAction call) {
            U.addAButton (index, text, call, true);
        }
        public static void addAButton (int index, string text, UnityAction call, bool interactable) {
            Button b = U.ui.cardViewUI.ctrl.buttons[index];
            b.GetComponentInChildren<Text> ().text = text;
            b.onClick.RemoveAllListeners ();
            b.onClick.AddListener (call);
            b.interactable = interactable;
            showAButton (b, index);
        }
    }
}
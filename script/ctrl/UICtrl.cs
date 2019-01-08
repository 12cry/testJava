using Cry.Common;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script.ctrl {
    public class UICtrl : Singleton<UICtrl> {
        protected override void Awake () {
            base.Awake ();
        }
        void Start () {
            maskImage.gameObject.SetActive (false);

        }
        public Image viewPanel;
        public Button bTakeCard;
        public Button bActionCard;
        public Button bCloseCard;

        public Image maskImage;

        public void mask (Transform[] transforms) {
            maskImage.gameObject.SetActive (true);
            maskImage.transform.SetAsLastSibling ();
            foreach (Transform t in transforms) {
                t.SetAsLastSibling ();
            }
        }
        public void unMask () {
            maskImage.gameObject.SetActive (false);
            // maskImage.transform.SetAsFirstSibling ();
        }
        public void takeCard () {
            Utils.currentCard.take ();
        }
        public void actionCard () {
            Utils.currentCard.action ();
        }
        public void closeCard () {
            Utils.currentCard.closeView ();
        }
        // public void hideBTakeCard () {
        //     bTakeCard.gameObject.SetActive (false);
        // }
        // public void hideBActionCard () {
        //     bActionCard.gameObject.SetActive (false);
        // }
        // public void hideBCloseCard () {
        //     bCloseCard.gameObject.SetActive (false);
        // }
        // public void showBTakeCard () {
        //     bTakeCard.gameObject.SetActive (true);
        // }
        // public void showBActionCard () {
        //     bActionCard.gameObject.SetActive (true);
        // }
        // public void showBCloseCard () {
        //     bCloseCard.gameObject.SetActive (true);
        // }
        public void showView () {
            viewPanel.gameObject.SetActive (true);
        }
        public void hideView () {
            viewPanel.gameObject.SetActive (false);
            // this.hideBTakeCard ();
            // this.hideBActionCard ();
            // this.hideBCloseCard ();
            this.unMask ();

        }
    }
}
using testCC.Assets.script.model;
using testJava.script.constant;
using testJava.script.ctrl.ui;
using testJava.script.model;
using testJava.script.model.ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script.ctrl {
    public class UICtrl : MonoBehaviour {
        public UI ui = new UI ();

        public Image maskBuildingImage;
        public Image maskCardImage;

        public PlayerUICtrl playerUICtrl;
        public CardViewUICtrl cardViewUICtrl;
        public OrgUICtrl orgUICtrl;
        public CityCardUICtrl cityCardUICtrl;

        public TextMeshProUGUI textPrefab;
        public CardCtrl cardCtrlPrefab;
        public CardCtrl cardCtrlBackgroud;
        public Text cardNumText;

        public void initTransform () {
            var scale = U.config.scale;

            var cardViewUICtrlRect = cardViewUICtrl.GetComponent<RectTransform> ().rect;
            var cityCardUICtrlRect = cityCardUICtrl.GetComponent<RectTransform> ().rect;

            cardViewUICtrl.transform.position = new Vector2 (Screen.width - cardViewUICtrlRect.width * scale / 2, Screen.height / 2);
            cityCardUICtrl.transform.position = new Vector2 (Screen.width - cityCardUICtrlRect.width * scale / 2, Screen.height / 2);
        }

        public void init () {
            ui.ctrl = this;
            U.ui = ui;

            playerUICtrl.init ();
            ui.playerUI = playerUICtrl.ui;

            orgUICtrl.init ();
            ui.orgUI = orgUICtrl.ui;

            cardViewUICtrl.init ();
            ui.cardViewUI = cardViewUICtrl.ui;

            cityCardUICtrl.init ();
            ui.cityCardUI = cityCardUICtrl.ui;

            // ui.closeAllView ();
            initTransform ();
        }

        public void viewResourceBuliding () {
            ui.viewBuildingCard (CardType.RESOURCE_BULIDING);
        }
        public void viewMilitaryBuliding () {
            ui.viewBuildingCard (CardType.MILITARY_BUILDING);
        }
        public void viewWonderBuliding () {
            ui.viewBuildingCard (CardType.WONDER);
        }
        public void closeViewBuilding () {
            ui.closeViewBuildingCard ();
        }

    }
}
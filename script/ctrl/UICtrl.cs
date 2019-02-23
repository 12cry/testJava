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

        public WarehouseUICtrl warehouseUICtrl;
        public StatisticUICtrl statisticUICtrl;
        public CardViewUICtrl cardViewUICtrl;
        public PopulationUICtrl populationUICtrl;
        public ActionUICtrl actionUICtrl;
        public OrgUICtrl orgUICtrl;
        // public MilitaryUICtrl militaryUICtrl;

        public TextMeshProUGUI textPrefab;
        public CardCtrl cardCtrlPrefab;
        public CardCtrl cardCtrlBackgroud;
        public Text cardNumText;

        public void initTransform () {
            var scale = U.config.scale;
            var populationUICtrlRect = populationUICtrl.GetComponent<RectTransform> ().rect;
            var warehouseUICtrlRect = warehouseUICtrl.GetComponent<RectTransform> ().rect;
            var statisticUICtrlRect = statisticUICtrl.GetComponent<RectTransform> ().rect;
            var actionUICtrlRect = actionUICtrl.GetComponent<RectTransform> ().rect;
            populationUICtrl.transform.position = new Vector2 (populationUICtrlRect.width * scale / 2, U.config.cardHeight + populationUICtrlRect.height * scale / 2);
            warehouseUICtrl.transform.position = new Vector2 ((populationUICtrlRect.width + warehouseUICtrlRect.width / 2 + 20) * scale, U.config.cardHeight + populationUICtrlRect.height * scale / 2);
            statisticUICtrl.transform.position = new Vector2 (Screen.width / 2, (Screen.height - statisticUICtrlRect.height * scale / 2));
            actionUICtrl.transform.position = new Vector2 (Screen.width - actionUICtrlRect.width * scale / 2, actionUICtrlRect.height * scale / 2);
        }

        public void init () {
            ui.ctrl = this;
            U.ui = ui;

            populationUICtrl.init ();
            ui.populationUI = populationUICtrl.ui;

            warehouseUICtrl.init ();
            ui.warehouseUI = warehouseUICtrl.ui;

            statisticUICtrl.init ();
            ui.statisticUI = statisticUICtrl.ui;

            actionUICtrl.init ();
            ui.actionUI = actionUICtrl.ui;

            orgUICtrl.init ();
            ui.orgUI = orgUICtrl.ui;

            cardViewUICtrl.init ();
            ui.cardViewUI = cardViewUICtrl.ui;

            // militaryUICtrl.init ();
            // ui.militaryUI = militaryUICtrl.ui;

            ui.closeAllView ();
            initTransform ();
        }

        public void viewResourceBuliding () {
            Debug.Log ("-------");
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
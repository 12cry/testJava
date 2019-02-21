using testCC.Assets.script.model;
using testJava.script.constant;
using testJava.script.ctrl.ui;
using testJava.script.model;
using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script.ctrl {
    public class UICtrl : MonoBehaviour {
        public UI ui = new UI ();

        public Image maskBuildingImage;

        public Text textPrefab;
        public CardCtrl cardCtrlPrefab;
        public CardCtrl cardCtrlBackgroud;
        public Text cardNumText;

        public WarehouseUICtrl warehouseUICtrl;
        // public ResourceUICtrl resourceUICtrl;
        public StatisticUICtrl statisticUICtrl;
        public CardViewUICtrl cardViewUICtrl;
        public PopulationUICtrl populationUICtrl;
        public ActionUICtrl actionUICtrl;
        public OrgUICtrl orgUICtrl;
        // public MilitaryUICtrl militaryUICtrl;

        public void init () {
            ui.ctrl = this;
            U.ui = ui;

            populationUICtrl.init ();
            ui.populationUI = populationUICtrl.ui;

            warehouseUICtrl.init ();
            ui.warehouseUI = warehouseUICtrl.ui;

            // resourceUICtrl.init ();
            // ui.resourceUI = resourceUICtrl.ui;

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
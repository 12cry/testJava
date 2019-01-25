using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.ctrl;
using testCC.Assets.script.model;
using testJava.script.constant;
using testJava.script.model.ui;
using UnityEngine;

namespace testJava.script.model {
    public class UI {
        public UICtrl ctrl;

        public ResourceUI resourceUI;
        public CardViewUI cardViewUI;
        public PopulationUI populationUI;
        public ActionUI actionUI;
        public WarehouseUI warehouseUI;
        public LeaderUI leaderUI;
        public MilitaryUI militaryUI;

        int cardType;
        public void viewBuilding (int cardType) {
            this.cardType = cardType;

            ctrl.maskBuildingImage.gameObject.SetActive (true);
            ctrl.maskBuildingImage.transform.SetAsLastSibling ();
            List<Building> buildings = U.world.getBuildings (cardType);
            for (int i = 0; i < buildings.Count; i++) {
                Building building = buildings[i];
                building.card.ctrl.gameObject.transform.localPosition = new Vector3 (i * 100 - 200, 0, 0);
                building.card.ctrl.gameObject.transform.SetAsLastSibling ();
            }
        }

        public void closeViewBuilding () {
            ctrl.maskBuildingImage.gameObject.SetActive (false);
            List<Building> buildings = U.world.getBuildings (cardType);
            for (int i = 0; i < buildings.Count; i++) {
                Building building = buildings[i];
                U.hideCard (building.card.ctrl);
            }
        }
        public void closeAllView () {
            cardViewUI.closeView ();
            closeViewBuilding ();
        }
    }
}
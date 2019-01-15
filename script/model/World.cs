using System.Collections.Generic;
using testCC.Assets.script.ctrl;
using testJava.script.constant;
using testJava.script.model;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script.model {
    public class World {
        public WorldCtrl ctrl;
        public List<Building> buildings = new List<Building> ();
        public Building[] farmBuildings = new Building[4];
        public Dictionary<int, Building> cardIdBuildingDic = new Dictionary<int, Building> ();

        public void build (Card card) {
            if (card.id == CardId.FARM1) {
                GameObject gameObject = Object.Instantiate<GameObject> (ctrl.farmPrefab, ctrl.transform);
                gameObject.transform.localPosition = new Vector3 (-1, 0, 0);

                Building building = new Building ();
                building.id = card.id;
                building.level = 0;
                building.card = card;
                building.gameObject = gameObject;
                building.workerNum = 0;
                building.init ();

                buildings.Add (building);
                farmBuildings[building.level] = building;
                cardIdBuildingDic.Add (card.id, building);
            }
            Utils.ui.resourceUI.updateCost (card.actionCost);

        }
        // public void workerToBuilding (int buildingId) {
        //     Building building = buildingDic[buildingId];
        //     RawImage worker = Utils.ui.populationUI.getAWorker ();
        //     building.addAWorker (worker);

        // }

    }
}
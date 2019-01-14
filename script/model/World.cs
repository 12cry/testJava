using System.Collections.Generic;
using testCC.Assets.script.ctrl;
using testJava.script.constant;
using testJava.script.model;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script.model {
    public class World {
        public WorldCtrl ctrl;
        List<Building> buildings = new List<Building> ();
        public Building[] farmBuildings = new Building[4];

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
            }
            Utils.ui.resourceUI.updateCost (card.actionCost);

        }
        public void viewBuilding () {
            Utils.ui.mask (new Transform[] { });
            for (int i = 0; i < buildings.Count; i++) {
                Building building = buildings[i];
                building.card.ctrl.gameObject.transform.localPosition = new Vector3 (i * 100, 0, 0);
                building.card.ctrl.gameObject.transform.SetAsLastSibling ();
            }
        }

        // public void workerToBuilding (int buildingId) {
        //     Building building = buildingDic[buildingId];
        //     RawImage worker = Utils.ui.populationUI.getAWorker ();
        //     building.addAWorker (worker);

        // }

    }
}
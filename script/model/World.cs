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

        public void build (CardBuild card) {
            Building building = buildABuilding (card);

            buildings.Add (building);
            farmBuildings[building.level] = building;
            cardIdBuildingDic.Add (card.id, building);
        }
        Building buildABuilding (CardBuild card) {

            Vector3 position = new Vector3 (0, 0, 0);
            int level = 0;
            if (card.id == CardId.FARM0) {
                position = new Vector3 (-1, 0, 0);
                level = 0;
            } else if (card.id == CardId.FARM1) {
                position = new Vector3 (-1, 2, 2);
                level = 1;
            }

            GameObject gameObject = Object.Instantiate<GameObject> (ctrl.farmPrefab, ctrl.transform);
            gameObject.transform.localPosition = position;

            Building building = new Building ();
            building.id = card.id;
            building.level = level;
            building.card = card;
            building.gameObject = gameObject;
            building.init ();

            return building;
        }

    }
}
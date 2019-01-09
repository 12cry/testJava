using System.Collections.Generic;
using testCC.Assets.script.ctrl;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script.model {
    public class World {
        public WorldCtrl ctrl;
        List<Building> buildings = new List<Building> ();
        public void build (Card card) {
            if (card.id == 1001) {
                GameObject gameObject = Object.Instantiate<GameObject> (ctrl.farmPrefab, ctrl.transform);
                gameObject.transform.localPosition = new Vector3 (0, 0, 0);

                Building building = new Building ();
                building.card = card;
                building.gameObject = gameObject;
                building.worker = 2;
                building.init ();

                buildings.Add (building);
            }

        }
        public void viewBuilding () {
            for (int i = 0; i < buildings.Count; i++) {
                Building building = buildings[i];
                building.card.ctrl.gameObject.transform.localPosition = new Vector3 (i * 100, 0, 0);
            }
        }
    }
}
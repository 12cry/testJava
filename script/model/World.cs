using System.Collections.Generic;
using testCC.Assets.script.ctrl;
using testJava.script.constant;
using testJava.script.model;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script.model {
    public class World {
        public WorldCtrl ctrl;
        public List<Building> resourceBuildings = new List<Building> ();
        public List<Building> militaryBuildings = new List<Building> ();
        // public Building[] farmBuildings = new Building[4];
        // public Dictionary<int, Building> cardIdBuildingDic = new Dictionary<int, Building> ();

        public List<Building> getBuildings (CardType cardType) {
            List<Building> buildings = null;
            if (cardType == CardType.ResourceBuliding) {
                buildings = resourceBuildings;
            } else if (cardType == CardType.MilitaryBuliding) {
                buildings = militaryBuildings;
            }
            return buildings;
        }
        // public void build (CardBuild card) {
        //     card.build ();
        //     getBuildings (card.cardType).Add (building);
        //     farmBuildings[building.level] = building;
        // }

    }
}
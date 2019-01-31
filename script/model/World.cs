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

        public List<Building> getBuildings (int cardType) {
            List<Building> buildings = null;

            if (cardType == CardType.RESOURCE_BULIDING) {
                buildings = resourceBuildings;
            } else if (cardType == CardType.MILITARY_UNIT) {
                buildings = militaryBuildings;
            }
            return buildings;
        }

    }
}
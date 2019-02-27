using System.Collections.Generic;
using testJava.script.constant;
using testJava.script.ctrl.ui;
using UnityEngine;

namespace testJava.script.model.ui {

    public class PlayerUI {
        public PlayerUICtrl ctrl;

        public StatisticUI statisticUI;
        public PopulationUI populationUI;
        public ActionUI actionUI;
        public WarehouseUI warehouseUI;
        public HandCardUI handCardUI;

        public List<BuildingCard> resourceWorkerBuildingCards = new List<BuildingCard> ();
        public List<BuildingCard> militaryWorkerBuildingCards = new List<BuildingCard> ();
        public List<BuildingCard> wonderBuildingCards = new List<BuildingCard> ();

        public List<BuildingCard> getBuildingCards (int cardType) {
            List<BuildingCard> cards = null;
            if (cardType == CardType.WONDER) {
                cards = wonderBuildingCards;
            } else if (cardType == CardType.RESOURCE_BULIDING) {
                cards = resourceWorkerBuildingCards;
            } else if (cardType == CardType.MILITARY_BUILDING) {
                cards = militaryWorkerBuildingCards;
            }
            return cards;
        }

    }
}
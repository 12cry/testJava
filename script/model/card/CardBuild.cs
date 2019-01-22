using testCC.Assets.script;
using testCC.Assets.script.model;
using testJava.script.constant;
using UnityEngine;

public class CardBuild : Card {

    public Cost actionCost;
    // public Income actionIncome;
    public Cost buildCost;
    public Income buildIncome;
    public BuildingType buildingType;
    public Building building;

    public override void undoAction () {
        base.undoAction ();
        U.world.undoBuild (this);
        U.ui.resourceUI.addCost (actionCost);

    }
    public override void action () {
        build ();
        U.ui.resourceUI.reduceCost (actionCost);
        base.action ();
    }
    public override void show () {
        base.show ();
        if (actionCost != null && actionCost.science != 0) {
            textDic["costScience"].text = actionCost.science.ToString ();
        }
    }
    public override bool actionAble () {
        if (U.ui.resourceUI.enough (actionCost)) {
            return true;
        }
        return false;
    }
    public override void displayActionButtons () {
        ResourceUI resourceUI = U.ui.resourceUI;
        if (buildingType == BuildingType.FARM) {

            int index = 0;
            U.addAButton (index++, "add a worker to farm1", delegate { building.addAWorker (); },
                U.ui.populationUI.workerNum > 0 && resourceUI.capacity >= building.card.buildCost.capacity);
            U.addAButton (index++, "remove a worker from farm1", delegate { building.removeWorker (); }, building.workerNum > 0);

            foreach (Building upgradeBuilding in U.world.resourceBuildings) {
                if (upgradeBuilding.card.buildingType == BuildingType.FARM && upgradeBuilding.level <= building.level) {
                    continue;
                }
                Cost cost = upgradeBuilding.card.buildCost.minus (building.card.buildCost);
                U.addAButton (index++, string.Format ("upgrade farm{0} to farm{1}", building.level, upgradeBuilding.level),
                    delegate { building.upgradeWorker (upgradeBuilding); }, resourceUI.enough (cost) && building.workerNum > 0);
            }
        }
    }
    public void build () {

        GameObject gameObject = Object.Instantiate<GameObject> (U.world.ctrl.farmPrefab);
        gameObject.transform.localPosition = getBuildingPosition ();

        Building building = new Building ();
        building.id = id;
        building.level = getBuildingLevel ();
        building.card = this;
        building.gameObject = gameObject;
        building.init ();

        this.building = building;
        U.world.getBuildings (cardType).Add (building);
    }
    public int getBuildingLevel () {
        int level = 0;
        if (id == CardId.FARM0) {
            level = 0;
        } else if (id == CardId.FARM1) {
            level = 1;
        }
        return level;
    }

    public Vector3 getBuildingPosition () {
        Vector3 position = Vector3.zero;
        if (id == CardId.FARM0) {
            position = new Vector3 (-1, 0, 0);
        } else if (id == CardId.FARM1) {
            position = new Vector3 (-1, 2, 2);
        }
        return position;
    }
}
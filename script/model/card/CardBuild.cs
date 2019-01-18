using testCC.Assets.script;
using testCC.Assets.script.model;
using testJava.script.constant;
using UnityEngine;

public class CardBuild : Card {

    public Cost actionCost;
    public Income actionIncome;
    public Cost buildCost;
    public Income buildIncome;
    public CardLevelType levelType;

    public override void action () {
        U.world.build (this);
        U.ui.resourceUI.updateCost (actionCost);
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
        if (levelType == CardLevelType.FARM) {

            Building[] buildings = U.world.farmBuildings;
            Building building = U.world.cardIdBuildingDic[id];

            int index = 0;
            U.addAButton (index++, "add a worker to farm1", delegate { building.addAWorker (); },
                U.ui.populationUI.workerNum > 0 && resourceUI.capacity >= building.card.buildCost.capacity);
            U.addAButton (index++, "remove a worker from farm1", delegate { building.removeWorker (); }, building.workerNum > 0);

            for (int i = building.level + 1; i < buildings.Length; i++) {
                Building upgradeBuilding = buildings[i];
                if (upgradeBuilding == null) {
                    continue;
                }
                Cost cost = upgradeBuilding.card.buildCost.minus (building.card.buildCost);
                U.addAButton (index++, string.Format ("upgrade farm{0} to farm{1}", building.level, upgradeBuilding.level),
                    delegate { building.upgradeWorker (upgradeBuilding); }, resourceUI.enough (cost) && building.workerNum > 0);
            }
        }
    }
}
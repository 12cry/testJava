using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.model;
using testJava.script.constant;
using testJava.script.model;
using UnityEngine;
using UnityEngine.UI;

public class WorkerBuildingCard : BuildingCard {
    public int buildingType;

    public Statistic buildCost;
    public Statistic buildIncome;

    public override void displayActionButtons () {

        ResourceUI resourceUI = U.ui.resourceUI;

        int index = 0;
        U.addAButton (index++, string.Format ("add a worker to {0}", building.name), delegate { addAWorker (); },
            U.ui.populationUI.workerNum > 0 && resourceUI.enough (buildCost));
        U.addAButton (index++, string.Format ("remove a worker from {0}", building.name), delegate { removeWorker (); }, building.workerNum > 0);

        foreach (Building upgradeBuilding in U.world.getBuildings (type)) {
            WorkerBuildingCard upgradeCard = ((WorkerBuildingCard) upgradeBuilding.card);
            if (upgradeCard.buildingType != buildingType || upgradeBuilding.level <= building.level) {
                continue;
            }
            U.addAButton (index++, string.Format ("upgrade {0} to {1}", building.name, upgradeBuilding.name),
                delegate { upgradeWorker (upgradeBuilding); },
                building.workerNum > 0 && resourceUI.enough (upgradeCard.buildCost.minus (buildCost)));
        }
    }

    public void upgradeWorker (Building building) {
        RawImage worker = this.building.workers.Dequeue ();
        building.workers.Enqueue (worker);

        this.building.updateWorkerNum (-1);
        building.updateWorkerNum (1);

        U.ui.reduce (buildIncome);
        U.ui.add (((WorkerBuildingCard) building.card).buildIncome);
        U.ui.actionUI.updateCivilRemainder (-1);
    }
    public void removeWorker () {
        RawImage worker = this.building.workers.Dequeue ();
        U.ui.populationUI.addWorker (worker);

        this.building.updateWorkerNum (-1);

        U.ui.reduce (buildIncome);
        U.ui.closeAllView ();
        U.ui.actionUI.updateCivilRemainder (-1);
    }
    public void addAWorker () {
        RawImage worker = U.ui.populationUI.getAWorker ();
        this.building.workers.Enqueue (worker);

        this.building.updateWorkerNum (1);

        U.ui.reduce (buildCost);
        U.ui.add (buildIncome);
        U.ui.closeAllView ();
        U.ui.actionUI.updateCivilRemainder (-1);
    }

}
using System.Collections.Generic;
using DG.Tweening;
using testCC.Assets.script;
using testCC.Assets.script.model;
using testJava.script.constant;
using testJava.script.model;
using UnityEngine;
using UnityEngine.UI;

public class WorkerBuildingCard : BuildingCard {
    public int buildingType;
    public int level;
    public Vector3 position;
    public Statistic buildCost;
    public Statistic buildIncome;

    int workerNum;

    public Queue<RawImage> workers = new Queue<RawImage> ();

    public override void action () {
        gameObject = Object.Instantiate<GameObject> (U.world.ctrl.farmPrefab);
        gameObject.transform.localPosition = position;
        U.ui.getBuildingCards (type).Add (this);
        this.updateWorkerNum (0);

        base.action ();
    }
    public override void render () {
        base.render ();
        U.setBuildCostText (ctrl, buildCost);
        U.setBuildIncomeText (ctrl, buildIncome);
    }

    public override void displayActionButtons () {

        int index = 0;
        U.addAButton (index++, string.Format ("add a worker to {0}", name), delegate { addAWorker (); },
            U.ui.populationUI.workerNum > 0 && U.ui.statisticUI.enough (buildCost));
        U.addAButton (index++, string.Format ("remove a worker from {0}", name), delegate { removeWorker (); }, workerNum > 0);

        foreach (WorkerBuildingCard upgradeCard in U.ui.getBuildingCards (type)) {
            if (upgradeCard.buildingType != buildingType || upgradeCard.level <= level) {
                continue;
            }
            U.addAButton (index++, string.Format ("upgrade {0} to {1}", name, upgradeCard.name),
                delegate { upgradeWorker (upgradeCard); },
                workerNum > 0 && U.ui.statisticUI.enough (upgradeCard.buildCost.minus (buildCost)));
        }
    }

    public void updateWorkerNum (int value) {
        workerNum += value;
        // TextMesh[] t = gameObject.GetComponentsInChildren<TextMesh> ();
        gameObject.GetComponentsInChildren<TextMesh> () [0].text = workerNum.ToString ();
    }
    public void upgradeWorker (WorkerBuildingCard card) {
        RawImage worker = this.workers.Dequeue ();
        this.updateWorkerNum (-1);

        card.workers.Enqueue (worker);
        card.updateWorkerNum (1);

        U.ui.statisticUI.reduce (card.buildCost);
        U.ui.statisticUI.add (buildCost);
        U.ui.statisticUI.reduce (buildIncome);
        U.ui.statisticUI.add (card.buildIncome);
        U.ui.closeAllView ();
        U.ui.actionUI.updateCivilRemainder (-1);
    }
    public void removeWorker () {
        RawImage worker = this.workers.Dequeue ();
        U.ui.populationUI.addWorker (worker);

        this.updateWorkerNum (-1);

        U.ui.statisticUI.reduce (buildIncome);
        U.ui.closeAllView ();
        U.ui.actionUI.updateCivilRemainder (-1);
    }
    public void addAWorker () {
        RawImage worker = U.ui.populationUI.getAWorker ();
        this.workers.Enqueue (worker);
        worker.transform.SetParent (worker.transform.parent.parent);
        worker.transform.DOMove (U.g.ctrl.mainCamera.WorldToScreenPoint (gameObject.transform.position), U.config.cardMoveSpeed);

        this.updateWorkerNum (1);

        U.ui.statisticUI.reduce (buildCost);
        U.ui.statisticUI.add (buildIncome);
        U.ui.closeAllView ();
        U.ui.actionUI.updateCivilRemainder (-1);
    }

}
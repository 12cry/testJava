using System.Collections.Generic;
using DG.Tweening;
using testCC.Assets.script;
using testCC.Assets.script.model;
using testJava.script.constant;
using testJava.script.model;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorkerBuildingCard : BuildingCard {
    public int buildingType;
    public int level;
    public Vector3 position;
    public Statistic buildCost;
    public Statistic buildIncome;

    public Statistic buildCostBackup;
    int workerNum;
    public Queue<RawImage> workers = new Queue<RawImage> ();

    public override void init () {
        base.init ();
        buildCostBackup = buildCost;
    }
    public override void initAction () {
        this.action ();

        U.cpUI.populationUI.idleToWorker ();
        addAWorker ();
    }
    public override void action () {
        gameObject = Object.Instantiate<GameObject> (U.world.ctrl.farmPrefab, U.cpWorld.ctrl.transform);
        gameObject.transform.localPosition = position;
        U.AddTriggerEvent (gameObject, EventTriggerType.PointerClick, delegate { U.ui.viewBuildingCard (type); });
        U.cpUI.getBuildingCards (type).Add (this);
        this.updateWorkerNum (0);

        base.action ();
    }
    public override void render () {
        base.render ();
        U.setBuildCostText (ctrl, buildCost);
        U.setBuildIncomeText (ctrl, buildIncome);
    }

    public override void displayBuildButtons () {

        int index = 0;
        U.addAButton (index++, string.Format ("c:{0},add a worker to {1}", buildCost.capacity, name), delegate { addAWorker (); },
            U.cpUI.populationUI.workerNum > 0 && U.cpUI.statisticUI.enough (buildCost));
        U.addAButton (index++, string.Format ("remove a worker from {0}", name), delegate { removeWorker (); }, workerNum > 0);

        foreach (WorkerBuildingCard upgradeCard in U.cpUI.getBuildingCards (type)) {
            if (upgradeCard.buildingType != buildingType || upgradeCard.level <= level) {
                continue;
            }
            U.addAButton (index++, string.Format ("upgrade {0} to {1}", name, upgradeCard.name),
                delegate { upgradeWorker (upgradeCard); },
                workerNum > 0 && U.cpUI.statisticUI.enough (upgradeCard.buildCost.minus (buildCost)));
        }
    }

    public void updateWorkerNum (int value) {
        workerNum += value;
        TextMeshPro[] t = gameObject.GetComponentsInChildren<TextMeshPro> ();
        t[0].SetText (workerNum.ToString ());
        // gameObject.GetComponentsInChildren<TextMeshPro> () [0].text = workerNum.ToString ();
    }
    public void upgradeWorker (WorkerBuildingCard card) {
        RawImage worker = this.workers.Dequeue ();
        this.updateWorkerNum (-1);

        card.workers.Enqueue (worker);
        card.updateWorkerNum (1);

        U.cpUI.statisticUI.reduce (card.buildCost);
        U.cpUI.statisticUI.add (buildCost);
        U.cpUI.statisticUI.computeMilitaryStatistic ();
        U.ui.closeAllView ();
        U.cpUI.actionUI.updateInteriorRemainder (-1);
    }
    public void removeWorker () {
        RawImage worker = this.workers.Dequeue ();
        U.cpUI.populationUI.addWorker (worker);

        this.updateWorkerNum (-1);

        U.cpUI.statisticUI.computeMilitaryStatistic ();
        U.ui.closeAllView ();
        U.cpUI.actionUI.updateInteriorRemainder (-1);
    }
    public void addAWorker () {
        RawImage worker = U.cpUI.populationUI.getAWorker ();
        this.workers.Enqueue (worker);
        worker.transform.SetParent (worker.transform.parent.parent);
        worker.transform.DOMove (U.g.ctrl.mainCamera.WorldToScreenPoint (gameObject.transform.position), U.config.cardMoveSpeed);

        this.updateWorkerNum (1);

        U.cpUI.statisticUI.reduce (buildCost);
        U.cpUI.statisticUI.computeMilitaryStatistic ();
        U.ui.closeAllView ();
        U.cpUI.actionUI.updateInteriorRemainder (-1);
    }

}
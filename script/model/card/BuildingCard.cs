using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.model;
using testJava.script.constant;
using testJava.script.model;
using UnityEngine;

public class BuildingCard : InteriorCard {

    public Statistic actionCost;
    public GameObject gameObject;

    public override void action () {
        U.cpUI.statisticUI.reduce (actionCost);
        ctrl.transform.parent = U.ui.ctrl.transform;
        base.action ();
    }
    public override void render () {
        base.render ();
        U.setActionCostText (ctrl, actionCost);
    }
    public override bool getActionAble () {
        if (U.cpUI.statisticUI.enough (actionCost)) {
            return true;
        }
        return false;
    }
    // public override void undoAction () {
    //     base.undoAction ();
    //     undoBuild ();
    //     U.ui.statisticUI.addCost (actionCost);
    // }
    // public void undoBuild () {
    //     Object.Destroy (building.gameObject);
    //     U.world.getBuildings (cardType).Remove (building);
    // }

}
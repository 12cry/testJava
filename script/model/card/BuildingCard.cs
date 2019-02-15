using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.model;
using testJava.script.constant;
using testJava.script.model;
using UnityEngine;

public class BuildingCard : Card {

    public Statistic actionCost;
    public GameObject gameObject;

    public override void action () {
        U.ui.reduce (actionCost);
        base.action ();
    }
    public override void render () {
        base.render ();
        U.setActionCostText (ctrl, actionCost);
    }
    public override bool actionAble () {
        if (U.ui.resourceUI.enough (actionCost)) {
            return true;
        }
        return false;
    }
    // public override void undoAction () {
    //     base.undoAction ();
    //     undoBuild ();
    //     U.ui.resourceUI.addCost (actionCost);
    // }
    // public void undoBuild () {
    //     Object.Destroy (building.gameObject);
    //     U.world.getBuildings (cardType).Remove (building);
    // }

}
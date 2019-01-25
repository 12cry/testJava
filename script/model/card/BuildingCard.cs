using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.model;
using testJava.script.constant;
using testJava.script.model;
using UnityEngine;

public class BuildingCard : Card {

    public Statistic actionCost;
    public Vector3 position;
    public int level;

    public Building building;
    public override void action () {
        build ();
        U.ui.reduce (actionCost);
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

    public void build () {
        GameObject gameObject = Object.Instantiate<GameObject> (U.world.ctrl.farmPrefab);
        gameObject.transform.localPosition = position;

        Building building = new Building ();
        building.id = id;
        building.level = level;
        building.card = this;
        building.name = name;
        building.gameObject = gameObject;
        building.init ();

        this.building = building;
        U.world.getBuildings (cardType).Add (building);
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
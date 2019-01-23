using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.model;
using testJava.script.constant;
using UnityEngine;

public class BuildingCard : Card {

    public Cost actionCost;
    public Building building;
    public Vector3 position;
    public int level;

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
    // int getBuildingLevel () {
    //     int level = 0;
    //     if (id == CardId.FARM0) {
    //         level = 0;
    //     } else if (id == CardId.FARM1) {
    //         level = 1;
    //     }
    //     return level;
    // }

    // Vector3 getBuildingPosition () {
    //     Vector3 position = Vector3.zero;
    //     if (id == CardId.FARM0) {
    //         position = new Vector3 (-1, 0, 0);
    //     } else if (id == CardId.FARM1) {
    //         position = new Vector3 (-1, 2, 2);
    //     } else if (id == CardId.MINE0) {
    //         position = new Vector3 (1, 2, 2);
    //     } else if (id == CardId.WARRIOR) {
    //         position = new Vector3 (-2, 2, 2);
    //     }
    //     return position;
    // }

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
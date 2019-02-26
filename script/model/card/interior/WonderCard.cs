using System.Collections;
using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.model;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.model.card {
    public class WonderCard : BuildingCard {

        public Statistic[] buildCosts;
        public Statistic buildIncome;
        Queue<RawImage> stageImages = new Queue<RawImage> ();
        int stage = 0;
        public override void action () {

            gameObject = Object.Instantiate<GameObject> (U.world.ctrl.farmPrefab);
            gameObject.transform.localPosition = new Vector3 (U.ui.getBuildingCards (type).Count, 0, 2);
            U.ui.getBuildingCards (type).Add (this);

            for (int i = 0; i < buildCosts.Length; i++) {
                RawImage stageImage = Object.Instantiate<RawImage> (U.cpUI.warehouseUI.ctrl.warehousePrefab, gameObject.transform);
                stageImage.transform.position = new Vector3 (i * 20, 0, 0);
                stageImages.Enqueue (stageImage);
            }

            base.action ();

        }
        public override void render () {
            base.render ();
            U.setBuildCostText (ctrl, buildCosts);
            U.setBuildIncomeText (ctrl, buildIncome);
        }

        public override void displayBuildButtons () {

            int capacity = 0;
            for (int i = 0; i < buildCosts.Length - stage; i++) {
                capacity += buildCosts[stage + i].capacity;
                int stageNum = i + 1;
                U.addAButton (i, string.Format ("build {0} stage with {1} civli use {2} c", i + 1, i + 1, capacity), delegate { buildStage (stageNum); },
                    U.cpUI.statisticUI.capacity >= capacity && U.cpUI.actionUI.civilRemainder > i);
            }

        }

        public void buildStage (int stageNum) {
            int capacity = 0;
            for (int i = 0; i < stageNum; i++) {
                capacity += buildCosts[stage + i].capacity;
                // stageImages.Dequeue ();
            }
            // U.cpUI.statisticUI.updateCapacity (-capacity);
            Statistic buildCost = new Statistic ();
            buildCost.capacity = capacity;
            U.cpUI.statisticUI.reduce (buildCost);

            stage += stageNum;
            U.cpUI.warehouseUI.updateWarehouse (-stageNum);
            if (stage == buildCosts.Length) {
                U.cpUI.warehouseUI.updateWarehouse (stage);
                U.cpUI.statisticUI.add (buildIncome);
            }

            U.cpUI.actionUI.updateCivilRemainder (-stageNum);
            U.ui.closeAllView ();
        }

    }
}
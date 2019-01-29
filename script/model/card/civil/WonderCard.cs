using System.Collections;
using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.model;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.model.card {
    public class WonderCard : BuildingCard {

        public int[] cCosts;
        Queue<RawImage> stageImages = new Queue<RawImage> ();
        int stage = 0;
        public override void action () {
            base.action ();

            for (int i = 0; i < cCosts.Length; i++) {
                RawImage stageImage = Object.Instantiate<RawImage> (U.ui.warehouseUI.ctrl.warehousePrefab, this.building.gameObject.transform);
                stageImage.transform.position = new Vector3 (i * 20, 0, 0);
                stageImages.Enqueue (stageImage);
            }

        }

        public override void displayActionButtons () {

            int cCost = 0;
            for (int i = 0; i < cCosts.Length - stage; i++) {
                cCost += cCosts[stage + i];
                U.addAButton (i, string.Format ("build {0} stage with {1} civli use {2} c", i + 1, i + 1, cCost), delegate { buildStage (cCosts.Length - stage); },
                    U.ui.resourceUI.capacity >= cCost && U.ui.actionUI.civilRemainder > i);
            }

        }

        public void buildStage (int stageNum) {

            int cCost = 0;
            for (int i = 0; i < stageNum; i++) {
                cCost += cCosts[stage + i];
                // stageImages.Dequeue ();
            }
            U.ui.resourceUI.updateCapacity (-cCost);

            stage += stageNum;
            U.ui.warehouseUI.updateWarehouse (-stageNum);
            if (stage == cCosts.Length) {
                U.ui.warehouseUI.updateWarehouse (stage);
            }

            U.ui.actionUI.updateCivilRemainder (-stageNum);
        }

    }
}
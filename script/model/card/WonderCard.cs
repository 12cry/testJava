using System.Collections;
using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.model;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.model.card {
    public class WonderCard : BuildingCard {

        public int[] capacityCosts;
        Queue<RawImage> stageImages = new Queue<RawImage> ();
        int stage = 0;
        public override void action () {
            base.action ();

            for (int i = 0; i < capacityCosts.Length; i++) {
                RawImage stageImage = Object.Instantiate<RawImage> (U.ui.warehouseUI.ctrl.warehousePrefab, this.building.gameObject.transform);
                stageImage.transform.position = new Vector3 (i * 20, 0, 0);
                stageImages.Enqueue (stageImage);
            }

        }

        public override void displayActionButtons () {

            int capacityCost = 0;
            for (int i = 0; i < capacityCosts.Length - stage; i++) {
                capacityCost += capacityCosts[stage + i];
                U.addAButton (i, string.Format ("build {0} stage with {1} civli use {2} capacity", i + 1, i + 1, capacityCost), delegate { buildStage (capacityCosts.Length - stage); },
                    U.ui.resourceUI.capacity >= capacityCost && U.ui.actionUI.civilRemainder > i);
            }

        }

        public void buildStage (int stageNum) {

            int capacityCost = 0;
            for (int i = 0; i < stageNum; i++) {
                capacityCost += capacityCosts[stage + i];
                // stageImages.Dequeue ();
            }
            U.ui.resourceUI.updateCapacity (-capacityCost);

            stage += stageNum;
            U.ui.warehouseUI.updateWarehouse (-stageNum);
            if (stage == capacityCosts.Length) {
                U.ui.warehouseUI.updateWarehouse (stage);
            }

            U.ui.actionUI.updateCivilRemainder (-stageNum);
        }

    }
}
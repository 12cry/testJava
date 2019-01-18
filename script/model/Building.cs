using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script.model {
    public class Building {
        public GameObject gameObject;
        Queue<RawImage> workers = new Queue<RawImage> ();
        public int id;
        public int level;

        public CardBuild card;
        public int workerNum = 0;

        public void init () {
            this.updateWorkerNum (0);
        }

        public void upgradeWorker (Building building) {
            RawImage worker = workers.Dequeue ();
            building.workers.Enqueue (worker);

            this.updateWorkerNum (-1);
            building.updateWorkerNum (1);

            U.ui.resourceUI.reduceIncome (card.buildIncome);
            U.ui.resourceUI.addIncome (building.card.buildIncome);
            U.ui.actionUI.updateCivilRemainder (-1);
        }
        public void removeWorker () {
            RawImage worker = workers.Dequeue ();
            U.ui.populationUI.addWorker (worker);

            this.updateWorkerNum (-1);

            U.ui.resourceUI.reduceIncome (card.buildIncome);
            U.ui.cardViewUI.closeAllView ();
            U.ui.actionUI.updateCivilRemainder (-1);
        }
        public void addAWorker () {
            RawImage worker = U.ui.populationUI.getAWorker ();
            workers.Enqueue (worker);

            worker.transform.parent = this.gameObject.transform;
            worker.transform.DOMove (new Vector3 (0, 0, 0), U.cardMoveSpeed);

            this.updateWorkerNum (1);

            U.ui.resourceUI.updateCost (card.buildCost);
            U.ui.resourceUI.addIncome (card.buildIncome);
            U.ui.cardViewUI.closeAllView ();
            U.ui.actionUI.updateCivilRemainder (-1);
        }
        public void updateWorkerNum (int value) {
            workerNum += value;
            TextMesh[] t = gameObject.GetComponentsInChildren<TextMesh> ();
            gameObject.GetComponentsInChildren<TextMesh> () [0].text = workerNum.ToString ();
        }
    }
}
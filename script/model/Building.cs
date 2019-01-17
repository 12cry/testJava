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

        public Card card;
        public int workerNum = 0;

        public void init () {
            this.updateWorkerNum (0);
        }

        public void upgradeWorker (Building building) {
            RawImage worker = workers.Dequeue ();
            building.workers.Enqueue (worker);

            this.updateWorkerNum (-1);
            building.updateWorkerNum (1);

            Utils.ui.resourceUI.reduceIncome (card.buildIncome);
            Utils.ui.resourceUI.addIncome (building.card.buildIncome);
            Utils.ui.actionUI.updateCivilRemainder (-1);
        }
        public void removeWorker () {
            RawImage worker = workers.Dequeue ();
            Utils.ui.populationUI.addWorker (worker);

            this.updateWorkerNum (-1);

            Utils.ui.resourceUI.reduceIncome (card.buildIncome);
            Utils.ui.cardViewUI.closeAllView ();
            Utils.ui.actionUI.updateCivilRemainder (-1);
        }
        public void addAWorker () {
            RawImage worker = Utils.ui.populationUI.getAWorker ();
            workers.Enqueue (worker);

            worker.transform.parent = this.gameObject.transform;
            worker.transform.DOMove (new Vector3 (0, 0, 0), Utils.cardMoveSpeed);

            this.updateWorkerNum (1);

            Utils.ui.resourceUI.updateCost (card.buildCost);
            Utils.ui.resourceUI.addIncome (card.buildIncome);
            Utils.ui.cardViewUI.closeAllView ();
            Utils.ui.actionUI.updateCivilRemainder (-1);
        }
        public void updateWorkerNum (int value) {
            workerNum += value;
            TextMesh[] t = gameObject.GetComponentsInChildren<TextMesh> ();
            gameObject.GetComponentsInChildren<TextMesh> () [0].text = workerNum.ToString ();
        }
    }
}
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
            this.getText ().text = workerNum.ToString ();
        }

        public void upgradeWorker (Building building) {
            RawImage worker = workers.Dequeue ();
            workerNum--;
            building.workers.Enqueue (worker);
            building.workerNum++;

            this.getText ().text = (workerNum -= 1).ToString ();
            building.getText ().text = (workerNum += 1).ToString ();

            Utils.ui.resourceUI.reduceIncome (card.buildIncome);
            Utils.ui.resourceUI.addIncome (building.card.buildIncome);
        }
        public void removeWorker () {
            RawImage worker = workers.Dequeue ();
            workerNum--;

            worker.transform.parent = Utils.ui.populationUI.ctrl.gameObject.transform;
            worker.transform.DOMove (new Vector3 (0, 0, 0), Utils.cardMoveSpeed);

            this.getText ().text = (workerNum -= 1).ToString ();

            Utils.ui.resourceUI.reduceIncome (card.buildIncome);
        }
        public void addAWorker () {
            RawImage worker = Utils.ui.populationUI.getAWorker ();
            workers.Enqueue (worker);
            workerNum++;

            worker.transform.parent = this.gameObject.transform;
            worker.transform.DOMove (new Vector3 (0, 0, 0), Utils.cardMoveSpeed);

            this.getText ().text = (workerNum += 1).ToString ();

            Utils.ui.resourceUI.updateCost (card.buildCost);
            Utils.ui.resourceUI.addIncome (card.buildIncome);
        }

        public TextMesh getText () {
            TextMesh[] tt = gameObject.GetComponentsInChildren<TextMesh> ();
            return tt[0];
        }
    }
}
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

        public void removeWorker () {
            RawImage worker = workers.Dequeue ();
            workerNum--;

        }
        public void addAWorker () {
            RawImage worker = Utils.ui.populationUI.getAWorker ();
            workers.Enqueue (worker);
            workerNum++;

            worker.transform.parent = this.gameObject.transform;
            worker.transform.DOMove (new Vector3 (0, 0, 0), Utils.cardMoveSpeed);

            this.getText ().text = (workerNum += 1).ToString ();
            Utils.ui.resourceUI.updateIncome (card.income);
        }

        public TextMesh getText () {
            TextMesh[] tt = gameObject.GetComponentsInChildren<TextMesh> ();
            return tt[0];
        }
    }
}
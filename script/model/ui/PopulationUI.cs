using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using testCC.Assets.script;
using testCC.Assets.script.model;
using testJava.script.constant;
using testJava.script.ctrl.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.model.ui {
    public class PopulationUI {
        public PopulationUICtrl ctrl;
        public int idleNum = 9;
        public int workerNum = 0;
        public Stack<RawImage> idles = new Stack<RawImage> ();
        public Stack<RawImage> workers = new Stack<RawImage> ();

        public void init () {
            for (int i = 0; i < idleNum; i++) {
                RawImage population = Object.Instantiate<RawImage> (ctrl.populationPrefab, ctrl.idleArea.transform);
                population.transform.localPosition = new Vector3 (i / 2 * 40, -i % 2 * 40, 0);
                idles.Push (population);
            }
        }

        public void idleToWorker () {
            int fCost = 3 - (idleNum - 1) / 4;
            if (U.currentLeader == CardId.LEADER_MZD) {
                fCost -= 1;
            }
            Statistic cost = new Statistic ();
            cost.food = fCost;
            if (!U.cpUI.statisticUI.enough (cost)) {
                return;
            }
            U.cpUI.statisticUI.reduce (cost);

            RawImage idle = idles.Pop ();
            idleNum -= 1;
            addWorker (idle);
            U.cpUI.actionUI.updateCivilRemainder (-1);
        }
        public void addWorker (RawImage worker) {
            workers.Push (worker);
            worker.transform.SetParent (ctrl.workerArea.transform);
            worker.transform.DOLocalMove (new Vector3 (workerNum / 2 * 40, -workerNum % 2 * 40, 0), U.config.cardMoveSpeed);
            workerNum++;
        }

        public RawImage getAWorker () {
            workerNum -= 1;
            return workers.Pop ();
        }
    }
}
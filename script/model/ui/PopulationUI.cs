using System.Collections;
using System.Collections.Generic;
using System.Threading;
using testJava.script.ctrl.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.model.ui {
    public class PopulationUI {
        public PopulationUICtrl ctrl;
        public int idle = 9;
        public int worker = 0;
        public Stack<RawImage> idles = new Stack<RawImage> ();
        public Stack<RawImage> workers = new Stack<RawImage> ();

        public void init () {
            for (int i = 0; i < idle; i++) {
                RawImage population = Object.Instantiate<RawImage> (ctrl.populationPrefab, ctrl.idleArea.transform);
                population.transform.localPosition = new Vector3 (i / 2 * 40, -i % 2 * 40, 0);
                idles.Push (population);
            }
        }

        public void addWorker () {
            RawImage idle = idles.Pop ();
            idle.transform.parent = this.ctrl.workerArea.transform;
            idle.transform.localPosition = new Vector3 (worker / 2 * 40, -worker % 2 * 40, 0);

            workers.Push (idle);
            worker += 1;
        }
    }
}
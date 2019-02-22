using testCC.Assets.script;
using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.ctrl.ui {
    public class PopulationUICtrl : MonoBehaviour {
        public PopulationUI ui;

        public Image idleArea;
        public Image workerArea;
        public RawImage populationPrefab;

        public void init () {

            ui = new PopulationUI ();
            ui.ctrl = this;
            ui.init ();
        }

        public void idleToWorker () {
            ui.idleToWorker ();
        }
    }
}
using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.ctrl.ui {
    public class PopulationUICtrl : MonoBehaviour {
        public PopulationUI ui = new PopulationUI ();

        public Image idleArea;
        public Image workerArea;
        public RawImage populationPrefab;

        void Start () {
            ui.ctrl = this;
            ui.init ();
        }

        public void addWorker () {
            ui.addWorker ();
        }
    }
}
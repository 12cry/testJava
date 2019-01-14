using Cry.Common;
using testCC.Assets.script.model;
using testJava.script.ctrl.ui;
using testJava.script.model;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script.ctrl {
    public class UICtrl : MonoBehaviour {
        public UI ui = new UI ();

        public ResourceUICtrl resourceUICtrl;
        public CardViewUICtrl cardViewUICtrl;
        public PopulationUICtrl populationUICtrl;

        public Image maskImage;

        void Start () {
            Debug.Log ("ui");
            ui.ctrl = this;
            ui.resourceUI = this.resourceUICtrl.ui;
            ui.cardViewUI = this.cardViewUICtrl.ui;
            ui.populationUI = this.populationUICtrl.ui;

            Utils.ui = ui;
        }

        public void click () {
            Debug.Log ("cccc");
        }

    }
}
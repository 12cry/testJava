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
        public ActionUICtrl actionUICtrl;
        public WarehouseUICtrl warrehouseUICtrl;
        public LeaderUICtrl leaderUICtrl;

        void Start () {
            ui.ctrl = this;
            ui.resourceUI = this.resourceUICtrl.ui;
            ui.cardViewUI = this.cardViewUICtrl.ui;
            ui.populationUI = this.populationUICtrl.ui;
            ui.actionUI = this.actionUICtrl.ui;
            ui.warehouseUI = this.warrehouseUICtrl.ui;
            ui.leaderUI = this.leaderUICtrl.ui;

            Utils.ui = ui;
        }

    }
}
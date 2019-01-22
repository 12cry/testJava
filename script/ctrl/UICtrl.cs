using testCC.Assets.script.model;
using testJava.script.ctrl.ui;
using testJava.script.model;
using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script.ctrl {
    public class UICtrl : MonoBehaviour {
        public UI ui = new UI ();

        public WarehouseUICtrl warehouseUICtrl;
        public ResourceUICtrl resourceUICtrl;
        public CardViewUICtrl cardViewUICtrl;
        public PopulationUICtrl populationUICtrl;
        public ActionUICtrl actionUICtrl;
        public LeaderUICtrl leaderUICtrl;
        public MilitaryUICtrl militaryUICtrl;

        void Start () {
            ui.ctrl = this;
            U.ui = ui;

            populationUICtrl.init ();
            ui.populationUI = populationUICtrl.ui;

            warehouseUICtrl.init ();
            ui.warehouseUI = warehouseUICtrl.ui;

            resourceUICtrl.init ();
            ui.resourceUI = resourceUICtrl.ui;

            actionUICtrl.init ();
            ui.actionUI = actionUICtrl.ui;

            leaderUICtrl.init ();
            ui.leaderUI = leaderUICtrl.ui;

            cardViewUICtrl.init ();
            ui.cardViewUI = cardViewUICtrl.ui;

            militaryUICtrl.init ();
            ui.militaryUI = militaryUICtrl.ui;

        }

    }
}
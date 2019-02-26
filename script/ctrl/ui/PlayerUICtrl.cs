using testCC.Assets.script;
using testJava.script.model.ui;
using UnityEngine;

namespace testJava.script.ctrl.ui {
    public class PlayerUICtrl : MonoBehaviour {
        public PlayerUI ui;

        public WarehouseUICtrl warehouseUICtrl;
        public StatisticUICtrl statisticUICtrl;
        public PopulationUICtrl populationUICtrl;
        public ActionUICtrl actionUICtrl;
        public HandCardUICtrl handCardUICtrl;
        public void initTransform () {
            var scale = U.config.scale;
            var populationUICtrlRect = populationUICtrl.GetComponent<RectTransform> ().rect;
            var warehouseUICtrlRect = warehouseUICtrl.GetComponent<RectTransform> ().rect;
            var statisticUICtrlRect = statisticUICtrl.GetComponent<RectTransform> ().rect;
            var actionUICtrlRect = actionUICtrl.GetComponent<RectTransform> ().rect;
            populationUICtrl.transform.position = new Vector2 (populationUICtrlRect.width * scale / 2, U.config.cardHeight + populationUICtrlRect.height * scale / 2);
            warehouseUICtrl.transform.position = new Vector2 ((populationUICtrlRect.width + warehouseUICtrlRect.width / 2 + 20) * scale, U.config.cardHeight + populationUICtrlRect.height * scale / 2);
            statisticUICtrl.transform.position = new Vector2 (Screen.width / 2, (Screen.height - statisticUICtrlRect.height * scale / 2));
            actionUICtrl.transform.position = new Vector2 (Screen.width - actionUICtrlRect.width * scale / 2, actionUICtrlRect.height * scale / 2);
        }
        public void init () {

            ui = new PlayerUI ();
            ui.ctrl = this;

            populationUICtrl.init ();
            ui.populationUI = populationUICtrl.ui;

            warehouseUICtrl.init ();
            ui.warehouseUI = warehouseUICtrl.ui;

            statisticUICtrl.init ();
            ui.statisticUI = statisticUICtrl.ui;

            actionUICtrl.init ();
            ui.actionUI = actionUICtrl.ui;

            handCardUICtrl.init ();
            ui.handCardUI = handCardUICtrl.ui;
            initTransform ();
        }
    }
}
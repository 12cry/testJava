using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.ctrl.ui {
    public class WarehouseUICtrl : MonoBehaviour {
        public WarehouseUI ui;
        public RawImage warehousePrefab;
        public void init () {
            ui = new WarehouseUI ();
            ui.ctrl = this;
            ui.init ();
        }
    }
}
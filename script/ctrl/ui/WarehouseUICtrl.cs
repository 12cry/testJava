using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.ctrl.ui {
    public class WarehouseUICtrl : MonoBehaviour {
        public WarehouseUI ui = new WarehouseUI ();
        public RawImage warehousePrefab;
        void Start () {
            ui.ctrl = this;
            ui.init ();
        }
    }
}
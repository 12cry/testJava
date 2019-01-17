using System.Collections.Generic;
using testJava.script.ctrl.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.model.ui {
    public class WarehouseUI {
        public WarehouseUICtrl ctrl;

        public int warehouseNum = 20;
        Stack<RawImage> warehouses = new Stack<RawImage> ();
        Stack<RawImage> warehousesOut = new Stack<RawImage> ();

        public void init () {
            for (int i = 0; i < warehouseNum; i++) {
                RawImage warehouse = Object.Instantiate<RawImage> (ctrl.warehousePrefab, ctrl.transform);
                warehouse.transform.localPosition = new Vector3 (i / 2 * 40, -i % 2 * 40, 0);
                warehouses.Push (warehouse);
            }
        }
        public void updateWarehouse (int value) {
            if (value > 0) {
                addWarehouse (value);
            }
            if (value < 0) {
                removeWarehouse (-value);
            }
        }
        void addWarehouse (int value) {
            for (int i = 0; i < value; i++) {
                RawImage warehouse = warehousesOut.Pop ();
                warehouses.Push (warehouse);
                warehouse.transform.localPosition = new Vector3 (warehouseNum / 2 * 40, -warehouseNum % 2 * 40, 0);
                warehouseNum++;
            }
        }
        void removeWarehouse (int value) {
            for (int i = 0; i < value; i++) {
                RawImage warehouse = warehouses.Pop ();
                warehousesOut.Push (warehouse);
                warehouse.transform.localPosition = new Vector3 (1000, 0, 0);
                warehouseNum--;
            }

        }

    }
}
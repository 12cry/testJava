using testCC.Assets.script;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.model.card {
    public class WonderBuilding : BuildingCard {

        public int[] buildCost;
        public override void action () {
            base.action ();

            foreach (int i in buildCost) {
                RawImage warehouse = Object.Instantiate<RawImage> (U.ui.warehouseUI.ctrl.warehousePrefab, this.building.gameObject.transform);
                warehouse.transform.position = new Vector3 (i * 20, 0, 0);
            }

        }

    }
}
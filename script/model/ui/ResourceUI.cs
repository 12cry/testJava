using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using testCC.Assets.script.ctrl;
using testJava.script.model;
using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script.model {
    public class ResourceUI {
        public ResourceUICtrl ctrl;

        public int food { get; set; }
        public int foodRaise { get; set; }
        public int capacity { get; set; }
        public int capacityRaise { get; set; }
        public int science { get; set; }
        public int scienceRaise { get; set; }
        public int culture { get; set; }
        public int cultureRaise { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }

        public void init () {
            Dictionary<string, int> dic = U.g.conf["resourceUI"];

            foreach (var d in dic) {
                this.GetType ().GetProperty (d.Key).SetValue (this, d.Value, null);
            }

            ctrl.foodText.text = this.food.ToString ();
            ctrl.foodRaiseText.text = this.foodRaise.ToString ();
            ctrl.capacityText.text = this.capacity.ToString ();
            ctrl.capacityRaiseText.text = this.capacityRaise.ToString ();
            ctrl.scienceText.text = this.science.ToString ();
            ctrl.scienceRaiseText.text = this.scienceRaise.ToString ();
            ctrl.cultureText.text = this.culture.ToString ();
            ctrl.cultureRaiseText.text = this.cultureRaise.ToString ();
            ctrl.attackText.text = this.attack.ToString ();
            ctrl.defenseText.text = this.defense.ToString ();

            U.ui.warehouseUI.removeWarehouse (this.food);
            U.ui.warehouseUI.removeWarehouse (this.capacity);
        }

        public void evaluating () {
            this.updateFood (foodRaise);
            this.updateCapacity (capacityRaise);
            this.updateScience (scienceRaise);
            this.updateCulture (cultureRaise);
        }
        public void reduce (Statistic statistic) {
            updateFoodRaise (-statistic.foodRaise);
            updateCapacityRaise (-statistic.capacityRaise);
            updateCultureRaise (-statistic.cultureRaise);
            updateScienceRaise (-statistic.scienceRaise);

            updateFood (-statistic.food);
            updateCapacity (-statistic.capacity);
            updateScience (-statistic.science);
            updateCulture (-statistic.culture);

            updateAttack (-statistic.attack);
            updateDefense (-statistic.defense);
        }
        public void add (Statistic statistic) {
            updateFoodRaise (statistic.foodRaise);
            updateCapacityRaise (statistic.capacityRaise);
            updateCultureRaise (statistic.cultureRaise);
            updateScienceRaise (statistic.scienceRaise);

            updateFood (statistic.food);
            updateCapacity (statistic.capacity);
            updateScience (statistic.science);
            updateCulture (statistic.culture);

            updateAttack (statistic.attack);
            updateDefense (statistic.defense);
        }
        public bool enough (Statistic statistic) {
            if (statistic.science > this.science || statistic.capacity > this.capacity) {
                return false;
            }
            return true;
        }

        public bool updateFood (int value) {
            if (food + value < 0) {
                return false;
            }
            WarehouseUI warehouseUI = U.ui.warehouseUI;
            if (warehouseUI.warehouseNum < value) {
                value = warehouseUI.warehouseNum;
            }
            warehouseUI.updateWarehouse (-value);
            this.food += value;
            ctrl.foodText.text = this.food.ToString ();
            return true;
        }

        public bool updateCapacity (int value) {
            if (capacity + value < 0) {
                return false;
            }
            WarehouseUI warehouseUI = U.ui.warehouseUI;
            if (warehouseUI.warehouseNum < value) {
                value = warehouseUI.warehouseNum;
            }
            warehouseUI.updateWarehouse (-value);

            this.capacity += value;
            ctrl.capacityText.text = this.capacity.ToString ();
            return true;
        }

        public void updateScience (int value) {
            this.science += value;
            ctrl.scienceText.text = this.science.ToString ();
        }

        public void updateCulture (int value) {
            this.culture += value;
            ctrl.cultureText.text = this.culture.ToString ();
        }

        public void updateFoodRaise (int value) {
            this.foodRaise += value;
            ctrl.foodRaiseText.text = this.foodRaise.ToString ();
        }

        public void updateCapacityRaise (int value) {
            this.capacityRaise += value;
            ctrl.capacityRaiseText.text = this.capacityRaise.ToString ();
        }

        public void updateScienceRaise (int value) {
            this.scienceRaise += value;
            ctrl.scienceRaiseText.text = this.scienceRaise.ToString ();
        }

        public void updateCultureRaise (int value) {
            this.cultureRaise += value;
            ctrl.cultureRaiseText.text = this.cultureRaise.ToString ();
        }
        public void updateAttack (int value) {
            if (value == 0) {
                return;
            }
            this.attack += value;
            ctrl.attackText.text = this.attack.ToString ();
        }

        public void updateDefense (int value) {
            if (value == 0) {
                return;
            }
            this.defense += value;
            ctrl.defenseText.text = this.defense.ToString ();
        }

    }
}
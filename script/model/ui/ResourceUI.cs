using System;
using testCC.Assets.script.ctrl;
using testJava.script.model;
using testJava.script.model.ui;

namespace testCC.Assets.script.model {
    public class ResourceUI {
        public ResourceUICtrl ctrl;

        public int food;
        public int foodRaise;
        public int capacity;
        public int capacityRaise;
        public int science;
        public int scienceRaise;
        public int culture;
        public int cultureRaise;
        public int attack;

        public void init () {
            this.updateFood (10);
            this.updateFoodRaise (0);
            this.updateCapacity (8);
            this.updateCapacityRaise (0);
            this.updateScience (15);
            this.updateScienceRaise (0);
            this.updateCulture (0);
            this.updateCultureRaise (0);
        }

        public void evaluating () {
            this.updateFood (foodRaise);
            this.updateCapacity (capacityRaise);
            this.updateScience (scienceRaise);
            this.updateCulture (cultureRaise);
        }
        public void reduce (Statistic statistic) {
            updateFoodRaise (-statistic.food);
            updateFoodRaise (-statistic.capacity);
            updateScience (-statistic.science);
            updateCapacity (-statistic.capacity);
            updateAttack (-statistic.attack);
        }
        public void add (Statistic statistic) {
            updateFoodRaise (statistic.food);
            updateFoodRaise (statistic.capacity);
            updateScience (statistic.science);
            updateCapacity (statistic.capacity);
            updateAttack (statistic.attack);
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

    }
}
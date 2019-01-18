using System;
using testCC.Assets.script.ctrl;
using testJava.script.model.ui;

namespace testCC.Assets.script.model {
    public class ResourceUI {
        public ResourceUICtrl ctrl;

        public int food;
        public int foodIncrement;
        public int capacity;
        public int capacityIncrement;
        public int science;
        public int scienceIncrement;
        public int culture;
        public int cultureIncrement;

        public void init () {
            this.updateFood (10);
            this.updateFoodIncrement (0);
            this.updateCapacity (8);
            this.updateCapacityIncrement (0);
            this.updateScience (5);
            this.updateScienceIncrement (0);
            this.updateCulture (0);
            this.updateCultureIncrement (0);
        }

        public void evaluating () {
            this.updateFood (foodIncrement);
            this.updateCapacity (capacityIncrement);
            this.updateScience (scienceIncrement);
            this.updateCulture (cultureIncrement);
        }
        public void addIncome (Income income) {
            updateFoodIncrement (income.food);
            updateFoodIncrement (income.capacity);
        }
        public void reduceIncome (Income income) {
            updateFoodIncrement (-income.food);
            updateFoodIncrement (-income.capacity);
        }
        public void updateCost (Cost cost) {
            updateScience (-cost.science);
            updateCapacity (-cost.capacity);
        }
        public bool enough (Cost cost) {
            if (cost.science > this.science || cost.capacity > this.capacity) {
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

        public void updateCapacity (int value) {
            this.capacity += value;
            ctrl.capacityText.text = this.capacity.ToString ();
        }

        public void updateScience (int value) {
            this.science += value;
            ctrl.scienceText.text = this.science.ToString ();
        }

        public void updateCulture (int value) {
            this.culture += value;
            ctrl.cultureText.text = this.culture.ToString ();
        }

        public void updateFoodIncrement (int value) {
            this.foodIncrement += value;
            ctrl.foodIncrementText.text = this.foodIncrement.ToString ();
        }

        public void updateCapacityIncrement (int value) {
            this.capacityIncrement += value;
            ctrl.capacityIncrementText.text = this.capacityIncrement.ToString ();
        }

        public void updateScienceIncrement (int value) {
            this.scienceIncrement += value;
            ctrl.scienceIncrementText.text = this.scienceIncrement.ToString ();
        }

        public void updateCultureIncrement (int value) {
            this.cultureIncrement += value;
            ctrl.cultureIncrementText.text = this.cultureIncrement.ToString ();
        }

    }
}
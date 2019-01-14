using System;
using testCC.Assets.script.ctrl;

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

        public int civ;
        public int civRemainder;

        public void updateIncome (Income income) {
            updateFoodIncrement (income.food);
        }
        public void updateCost (Cost cost) {
            updateScience (-cost.science);
        }

        public void updateFoodIncrementDisplay () {
            ctrl.foodIncrementText.text = this.foodIncrement.ToString ();
        }
        public void updateCapacityIncrementDisplay () {
            ctrl.capacityIncrementText.text = this.capacityIncrement.ToString ();
        }
        public void updateScienceIncrementDisplay () {
            ctrl.scienceIncrementText.text = this.scienceIncrement.ToString ();
        }
        public void updateCultureIncrementDisplay () {
            ctrl.cultureIncrementText.text = this.cultureIncrement.ToString ();
        }

        public void updateScienceDisplay () {
            ctrl.scienceText.text = this.science.ToString ();
        }
        public void updateCivDisplay () {
            ctrl.civText.text = this.civ.ToString ();
            ctrl.civTextRemainder.text = this.civRemainder.ToString ();
        }

        public void updateAllDisplay () {
            this.updateRemainderDisplay ();
            this.updateFoodIncrementDisplay ();
            this.updateCapacityIncrementDisplay ();
            this.updateScienceIncrementDisplay ();
            this.updateCultureIncrementDisplay ();
            this.updateCivDisplay ();
        }
        public void updateRemainderDisplay () {
            ctrl.foodText.text = this.food.ToString ();
            ctrl.capacityText.text = this.capacity.ToString ();
            ctrl.scienceText.text = this.science.ToString ();
            ctrl.cultureText.text = this.culture.ToString ();
        }

        public void init () {
            this.food = 0;
            this.foodIncrement = 2;
            this.capacity = 0;
            this.capacityIncrement = 2;
            this.science = 0;
            this.scienceIncrement = 2;
            this.culture = 0;
            this.cultureIncrement = 0;
            this.civ = 4;
            this.civRemainder = 4;
            this.updateAllDisplay ();
        }
        public void updateFoodIncrement (int value) {
            if (value == 0) {
                return;
            }
            this.foodIncrement += value;
            this.updateFoodIncrementDisplay ();
        }

        public void updateCapacityIncrement (int value) {
            if (value == 0) {
                return;
            }
            this.capacityIncrement += value;
            this.updateCapacityIncrementDisplay ();
        }

        public void updateScienceIncrement (int value) {
            if (value == 0) {
                return;
            }
            this.scienceIncrement += value;
            this.updateScienceIncrementDisplay ();
        }

        public void updateCultureIncrement (int value) {
            if (value == 0) {
                return;
            }
            this.cultureIncrement += value;
            this.updateCultureIncrementDisplay ();
        }

        public void updateScience (int value) {
            if (value == 0) {
                return;
            }
            this.science += value;
            this.updateScienceDisplay ();

        }

        public void updateRemainder () {
            food += foodIncrement;
            capacity += capacityIncrement;
            science += scienceIncrement;
            culture += cultureIncrement;
            updateRemainderDisplay ();
        }
        public void updateCiv (int value) {
            if (value == 0) {
                return;
            }

            this.civRemainder -= value;
            this.updateCivDisplay ();
        }

    }
}
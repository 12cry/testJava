using System;

namespace testCC.Assets.script.model {
    public class Resource {
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

        public event Action updateFoodIncrementDisplay;
        public event Action updateCapacityIncrementDisplay;
        public event Action updateScienceIncrementDisplay;
        public event Action updateCultureIncrementDisplay;

        public event Action updateScienceDisplay;
        public event Action updateRemainderDisplay;
        public event Action updateAllDisplay;

        public event Action updateCivDisplay;

        public void updateCiv (int value) {
            if (value == 0) {
                return;
            }

            this.civRemainder -= value;
            this.updateCivDisplay ();
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
    }
}
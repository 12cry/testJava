using Cry.Common;
using testCC.Assets.script.model;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script.ctrl {
    public class ResourceCtrl : MonoBehaviour {

        public Text foodText;
        public Text foodIncrementText;
        public Text capacityText;
        public Text capacityIncrementText;
        public Text scienceText;
        public Text scienceIncrementText;
        public Text cultureText;
        public Text cultureIncrementText;

        public Text civText;
        public Text civTextRemainder;

        public Resource resource;

        void Awake () {
            resource = new Resource ();
            Utils.resource = resource;

            resource.updateFoodIncrementDisplay += updateFoodIncrementDisplay;
            resource.updateCapacityIncrementDisplay += updateCapacityIncrementDisplay;
            resource.updateScienceIncrementDisplay += updateScienceIncrementDisplay;
            resource.updateCultureIncrementDisplay += updateCultureIncrementDisplay;

            resource.updateScienceDisplay += updateScienceDisplay;
            resource.updateRemainderDisplay += updateRemainderDisplay;
            resource.updateAllDisplay += updateAllDisplay;

            resource.updateCivDisplay += updateCivDisplay;

            resource.init ();
        }

        public void updateCivDisplay () {
            civText.text = this.resource.civ.ToString ();
            civTextRemainder.text = this.resource.civRemainder.ToString ();
        }
        public void updateFoodIncrementDisplay () {
            foodIncrementText.text = this.resource.foodIncrement.ToString ();
        }
        public void updateCapacityIncrementDisplay () {
            capacityIncrementText.text = this.resource.capacityIncrement.ToString ();
        }
        public void updateScienceIncrementDisplay () {
            scienceIncrementText.text = this.resource.scienceIncrement.ToString ();
        }
        public void updateCultureIncrementDisplay () {
            cultureIncrementText.text = this.resource.cultureIncrement.ToString ();
        }

        public void updateScienceDisplay () {
            scienceText.text = this.resource.science.ToString ();
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
            this.foodText.text = this.resource.food.ToString ();
            this.capacityText.text = this.resource.capacity.ToString ();
            this.scienceText.text = this.resource.science.ToString ();
            this.cultureText.text = this.resource.culture.ToString ();
        }
    }
}
using testCC.Assets.script.model;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script.ctrl {
    public class ResourceUICtrl : MonoBehaviour {
        public ResourceUI ui = new ResourceUI ();

        public Text foodText;
        public Text foodIncrementText;
        public Text capacityText;
        public Text capacityIncrementText;
        public Text scienceText;
        public Text scienceIncrementText;
        public Text cultureText;
        public Text cultureIncrementText;
        public Text attackText;
        public Text defenseText;

        public void init () {
            ui.ctrl = this;
            ui.init ();
        }

    }
}
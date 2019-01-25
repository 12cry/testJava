using testCC.Assets.script.model;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script.ctrl {
    public class ResourceUICtrl : MonoBehaviour {
        public ResourceUI ui = new ResourceUI ();

        public Text foodText;
        public Text foodRaiseText;
        public Text capacityText;
        public Text capacityRaiseText;
        public Text scienceText;
        public Text scienceRaiseText;
        public Text cultureText;
        public Text cultureRaiseText;
        public Text attackText;
        public Text defenseText;

        public void init () {
            ui.ctrl = this;
            ui.init ();
        }

    }
}
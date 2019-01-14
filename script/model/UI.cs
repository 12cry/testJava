using testCC.Assets.script;
using testCC.Assets.script.ctrl;
using testCC.Assets.script.model;
using testJava.script.model.ui;
using UnityEngine;

namespace testJava.script.model {
    public class UI {
        public UICtrl ctrl;

        public ResourceUI resourceUI;
        public CardViewUI cardViewUI;
        public PopulationUI populationUI;

        public void mask (Transform[] transforms) {
            ctrl.maskImage.gameObject.SetActive (true);
            ctrl.maskImage.transform.SetAsLastSibling ();
            foreach (Transform t in transforms) {
                t.SetAsLastSibling ();
            }
        }
        public void unMask () {
            ctrl.maskImage.gameObject.SetActive (false);
        }
    }
}
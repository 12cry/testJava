using testCC.Assets.script.model;
using UnityEngine;

namespace testCC.Assets.script.ctrl {
    public class WorldCtrl : MonoBehaviour {
        World world = new World ();
        public GameObject farmPrefab;

        void Start () {
            world.ctrl = this;
            Utils.world = world;

        }
        public void viewBuilding () {
            world.viewBuilding ();
        }
    }
}
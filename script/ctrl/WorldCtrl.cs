using testCC.Assets.script.model;
using UnityEngine;

namespace testCC.Assets.script.ctrl {
    public class WorldCtrl : MonoBehaviour {
        public GameObject farmPrefab;

        void Start () {
            World world = new World ();
            world.ctrl = this;
            Utils.world = world;

        }
    }
}
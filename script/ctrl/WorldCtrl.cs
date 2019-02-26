using testCC.Assets.script;
using testCC.Assets.script.model;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.ctrl {
    public class WorldCtrl : MonoBehaviour {
        World world = new World ();
        public PlayerWorldCtrl playerWorldCtrl;
        public GameObject farmPrefab;

        public void init () {
            world.ctrl = this;
            U.world = world;

            playerWorldCtrl.init ();
            world.playerWorld = playerWorldCtrl.world;
        }
    }
}
using testJava.script.model;
using UnityEngine;

namespace testJava.script.ctrl {

    public class PlayerWorldCtrl : MonoBehaviour {
        public PlayerWorld world;
        public void init () {
            world = new PlayerWorld ();
            world.ctrl = this;
        }
    }
}
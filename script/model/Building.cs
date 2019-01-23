using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script.model {

    public class Building {
        public GameObject gameObject;
        public Queue<RawImage> workers = new Queue<RawImage> ();
        public int id;
        public int level;
        public string name;

        public BuildingCard card;
        public int workerNum = 0;

        public void init () {
            this.updateWorkerNum (0);
        }

        public void updateWorkerNum (int value) {
            workerNum += value;
            TextMesh[] t = gameObject.GetComponentsInChildren<TextMesh> ();
            gameObject.GetComponentsInChildren<TextMesh> () [0].text = workerNum.ToString ();
        }
    }
}
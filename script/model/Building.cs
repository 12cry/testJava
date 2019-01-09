using UnityEngine;

namespace testCC.Assets.script.model {
    public class Building {
        public GameObject gameObject;
        public Card card;
        public int worker = 0;

        public void init () {
            this.getText ().text = worker.ToString ();
        }

        public void addAWorker () {
            this.getText ().text = (worker += 1).ToString ();
        }

        public TextMesh getText () {
            TextMesh[] tt = gameObject.GetComponentsInChildren<TextMesh> ();
            return tt[0];
        }
    }
}
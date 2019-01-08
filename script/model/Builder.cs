using UnityEngine;

namespace testCC.Assets.script.model {
    public class Builder {
        public GameObject gameObject;
        public int peopleNum = 0;

        public void init () {
            this.getText ().text = peopleNum.ToString ();
        }

        public void addPeople (int value) {
            this.getText ().text = (peopleNum += value).ToString ();
        }

        public TextMesh getText () {
            TextMesh[] tt = gameObject.GetComponentsInChildren<TextMesh> ();
            return tt[0];
        }
    }
}
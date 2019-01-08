using System.Collections.Generic;
using testCC.Assets.script.ctrl;
using UnityEngine;
using UnityEngine.UI;

namespace testCC.Assets.script.model {
    public class World {
        public WorldCtrl ctrl;
        Dictionary<int, Builder> builderDic = new Dictionary<int, Builder> ();
        public void build (int cardId) {
            if (cardId == 1001) {
                GameObject go = Object.Instantiate<GameObject> (ctrl.farmPrefab, ctrl.transform);
                go.transform.localPosition = new Vector3 (0, 0, 0);
                TextMesh[] tt = go.GetComponentsInChildren<TextMesh> ();
                tt[0].text = "0";

                Builder builder = new Builder ();
                // builder.go = go;
                builderDic.Add (cardId, builder);
            }

        }
    }
}
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
                GameObject gameObject = Object.Instantiate<GameObject> (ctrl.farmPrefab, ctrl.transform);
                gameObject.transform.localPosition = new Vector3 (0, 0, 0);

                Builder builder = new Builder ();
                builder.gameObject = gameObject;
                builder.init ();

                builderDic.Add (cardId, builder);
            }

        }
    }
}
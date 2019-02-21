using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace testCC.Assets.script {
    public class CardCtrl : MonoBehaviour, IPointerClickHandler {
        public Card card;
        public Text cardNameText;
        public RawImage header;
        public RawImage footer;

        public void OnPointerClick (PointerEventData eventData) {
            card.view ();
        }

    }
}
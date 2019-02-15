using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace testCC.Assets.script {
    public class CardCtrl : MonoBehaviour, IPointerClickHandler {
        public Card card;
        public Text cardNameText;
        public Image header;
        public Image footer;

        public void OnPointerClick (PointerEventData eventData) {
            card.view ();
        }

    }
}
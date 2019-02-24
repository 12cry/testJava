using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace testCC.Assets.script {
    public class CardCtrl : MonoBehaviour, IPointerClickHandler {
        public Card card;
        public TextMeshProUGUI cardNameText;
        public RawImage header;
        public RawImage image;
        public RawImage footer;

        public void OnPointerClick (PointerEventData eventData) {
            card.view ();
        }

    }
}
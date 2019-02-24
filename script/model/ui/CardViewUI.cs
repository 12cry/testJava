using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.model;
using testJava.script.constant;
using testJava.script.ctrl.ui;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace testJava.script.model.ui {
    public class CardViewUI {
        public CardViewUICtrl ctrl;
        public ViewState state = ViewState.HIDE;
        public Button bTakeCard;
        public Button bActionCard;
        public List<Button> buttons = new List<Button> ();

        CardType cardType;
        public void init () {
            closeView ();

            bActionCard = Object.Instantiate<Button> (ctrl.buttonParfab, ctrl.transform);
            bActionCard.GetComponentInChildren<TextMeshProUGUI> ().SetText ("action");
            bActionCard.onClick.AddListener (delegate { ctrl.actionCard (); });

            Button b = Object.Instantiate<Button> (ctrl.buttonParfab, ctrl.transform);
            buttons.Add (b);
            b = Object.Instantiate<Button> (ctrl.buttonParfab, ctrl.transform);
            buttons.Add (b);
            b = Object.Instantiate<Button> (ctrl.buttonParfab, ctrl.transform);
            buttons.Add (b);

        }
        public void view () {
            state = ViewState.SHOW;
            Card card = U.currentCard;

            U.ui.ctrl.maskCardImage.gameObject.SetActive (true);
            U.ui.ctrl.maskCardImage.transform.SetAsLastSibling ();
            ctrl.gameObject.SetActive (true);
            ctrl.transform.SetAsLastSibling ();
            card.ctrl.transform.SetAsLastSibling ();
            ctrl.desc.text = card.desc;

        }

        public void hideAllButton () {
            Button[] bb = ctrl.GetComponentsInChildren<Button> ();
            System.Array.ForEach (bb, b => {
                b.transform.localPosition = new Vector3 (1000, 0, 0);
            });
        }
        public void closeView () {
            state = ViewState.HIDE;
            ctrl.gameObject.SetActive (false);
            U.ui.ctrl.maskCardImage.gameObject.SetActive (false);
            hideAllButton ();
        }

    }
}
using System;
using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.model;
using testCC.Assets.script.model.card;
using testJava.script.constant;
using testJava.script.ctrl.ui;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace testJava.script.model.ui {
    public class CardViewUI {
        public CardViewUICtrl ctrl;
        public ViewState state = ViewState.HIDE;

        CardType cardType;
        public void init () {
            closeView ();
        }
        public void view () {
            state = ViewState.SHOW;
            Card card = U.currentCard;

            ctrl.maskCardImage.gameObject.SetActive (true);
            ctrl.maskCardImage.transform.SetAsLastSibling ();
            ctrl.gameObject.SetActive (true);
            ctrl.transform.SetAsLastSibling ();
            card.ctrl.transform.SetAsLastSibling ();
            ctrl.desc.text = card.desc;

        }

        public void hideAllButton () {
            Button[] bb = ctrl.GetComponentsInChildren<Button> ();
            Array.ForEach (bb, b => {
                b.transform.localPosition = new Vector3 (1000, 0, 0);
            });
        }
        public void closeView () {
            state = ViewState.HIDE;
            ctrl.gameObject.SetActive (false);
            ctrl.maskCardImage.gameObject.SetActive (false);
            hideAllButton ();
        }

    }
}
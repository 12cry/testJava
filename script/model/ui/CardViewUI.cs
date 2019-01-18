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

        public void init () {
            closeView ();
        }
        public void view () {
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
            ctrl.gameObject.SetActive (false);
            ctrl.maskCardImage.gameObject.SetActive (false);
            hideAllButton ();
        }

        public void viewBuilding () {
            ctrl.maskBuildingImage.gameObject.SetActive (true);
            ctrl.maskBuildingImage.transform.SetAsLastSibling ();
            List<Building> buildings = U.world.buildings;
            for (int i = 0; i < buildings.Count; i++) {
                Building building = buildings[i];
                building.card.ctrl.gameObject.transform.localPosition = new Vector3 (i * 100 - 200, 0, 0);
                building.card.ctrl.gameObject.transform.SetAsLastSibling ();
            }
        }

        public void closeViewBuilding () {
            ctrl.maskBuildingImage.gameObject.SetActive (false);
            List<Building> buildings = U.world.buildings;
            for (int i = 0; i < buildings.Count; i++) {
                Building building = buildings[i];
                U.hideCard (building.card.ctrl);
            }
        }
        public void closeAllView () {
            closeView ();
            closeViewBuilding ();
        }

    }
}
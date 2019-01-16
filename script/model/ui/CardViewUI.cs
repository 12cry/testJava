using System;
using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.model;
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
        void showAButton (Button b, int index) {
            b.transform.localPosition = new Vector3 (0, 30 * index, 0);
        }
        void addAButton (int index, string text, UnityAction call) {
            Button b = ctrl.buttons[index];
            b.GetComponentInChildren<Text> ().text = text;
            b.onClick.RemoveAllListeners ();
            b.onClick.AddListener (call);
            showAButton (b, index++);
        }
        public void displayActionButtons () {
            Card card = Utils.currentCard;

            int index = 0;
            ResourceUI resourceUI = Utils.ui.resourceUI;
            if (card.levelType == CardLevelType.FARM) {

                Building[] buildings = Utils.world.farmBuildings;
                Building building = Utils.world.cardIdBuildingDic[card.id];
                Debug.Log (Utils.ui.populationUI.workerNum);
                Debug.Log (resourceUI.capacity);
                Debug.Log (building.card.buildCost.capacity);
                if (Utils.ui.populationUI.workerNum > 0 && resourceUI.capacity >= building.card.buildCost.capacity) {
                    addAButton (index++, "add a worker to farm1", delegate { building.addAWorker (); });
                }
                if (building.workerNum > 0) {
                    addAButton (index++, "remove a worker from farm1", delegate { building.removeWorker (); });
                }

                for (int i = building.level + 1; i < buildings.Length; i++) {
                    if (buildings[i] == null) {
                        continue;
                    }
                    Building upgradeBuilding = buildings[i];
                    if (resourceUI.enough (upgradeBuilding.card.buildCost.minus (building.card.buildCost))) {
                        addAButton (index++, "upgrade a worker from farm1", delegate { building.upgradeWorker (building); });
                    }
                }
            }
        }
        public void view () {

            ctrl.maskCardImage.gameObject.SetActive (true);
            ctrl.maskCardImage.transform.SetAsLastSibling ();
            ctrl.gameObject.SetActive (true);
            ctrl.transform.SetAsLastSibling ();
            Utils.currentCard.ctrl.transform.SetAsLastSibling ();
            Card card = Utils.currentCard;
            ctrl.desc.text = card.desc;

            int civilRemainder = Utils.ui.actionUI.civilRemainder;
            if (civilRemainder == 0) {
                return;
            }

            int index = 0;
            if (card.state == CardState.SHOWING) {
                if (civilRemainder >= card.takeCivil) {
                    showAButton (ctrl.bTakeCard, index++);
                }
            } else if (card.state == CardState.TAKED) {
                if (Utils.ui.resourceUI.enough (card.actionCost)) {
                    showAButton (ctrl.bActionCard, index++);
                }
            } else if (card.state == CardState.ACTINGED) {
                displayActionButtons ();
            }

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
            List<Building> buildings = Utils.world.buildings;
            for (int i = 0; i < buildings.Count; i++) {
                Building building = buildings[i];
                building.card.ctrl.gameObject.transform.localPosition = new Vector3 (i * 100 - 200, 0, 0);
                building.card.ctrl.gameObject.transform.SetAsLastSibling ();
            }
        }

        public void closeViewBuilding () {
            ctrl.maskBuildingImage.gameObject.SetActive (false);
            List<Building> buildings = Utils.world.buildings;
            for (int i = 0; i < buildings.Count; i++) {
                Building building = buildings[i];
                Utils.hideCard (building.card.ctrl);
            }
        }
        public void closeAllView () {
            closeView ();
            closeViewBuilding ();
        }

    }
}
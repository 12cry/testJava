using System;
using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.model;
using testJava.script.constant;
using testJava.script.ctrl.ui;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace testJava.script.model.ui {
    public class CardViewUI {
        public CardViewUICtrl ctrl;

        void showAButton (Button b, int index) {
            b.transform.localPosition = new Vector3 (0, 30 * index, 0);
        }

        public void displayActionButtons () {
            Card card = Utils.currentCard;

            int index = 0;

            if (card.levelType == CardLevelType.FARM) {

                List<Building> buildings = Utils.world.farmBuilding;
                foreach (Building building in buildings) {

                    Button b0 = ctrl.buttons[0];
                    b0.GetComponentInChildren<Text> ().text = "add a worker to farm1";
                    b0.onClick.AddListener (delegate { building.addAWorker (); });
                    showAButton (b0, index++);

                    Button b1 = ctrl.buttons[1];
                    b1.GetComponentInChildren<Text> ().text = "remove a worker from farm1";
                    b1.onClick.AddListener (delegate { building.removeWorker (); });
                    showAButton (b1, index++);

                    if (Utils.ui.populationUI.workerNum == 0) {
                        b0.interactable = false;
                    }

                    if (building.workerNum == 0) {
                        b1.interactable = false;
                    }

                }

            }

        }
        public void showView () {
            Card card = Utils.currentCard;
            ctrl.desc.text = card.desc;
            int index = 0;

            hideAllButton ();
            if (card.state == CardState.SHOWING) {
                showAButton (ctrl.bTakeCard, index++);
            } else if (card.state == CardState.TAKED) {
                showAButton (ctrl.bActionCard, index++);
            } else if (card.state == CardState.ACTINGED) {
                displayActionButtons ();
            }

            ctrl.gameObject.SetActive (true);
            Utils.ui.mask (new Transform[] { Utils.currentCard.ctrl.transform, ctrl.transform });
        }
        public void hideAllButton () {
            Button[] bb = ctrl.GetComponentsInChildren<Button> ();
            Array.ForEach (bb, b => {
                b.transform.localPosition = new Vector3 (1000, 0, 0);
            });
        }
        public void hideView () {
            ctrl.gameObject.SetActive (false);
            Utils.ui.unMask ();
        }

        public void buttonAble (bool takeable, bool actionable) {
            ctrl.bTakeCard.gameObject.SetActive (takeable);
            ctrl.bActionCard.gameObject.SetActive (actionable);

        }

    }
}
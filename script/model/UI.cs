using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.ctrl;
using testCC.Assets.script.model;
using testJava.script.constant;
using testJava.script.model.ui;
using UnityEngine;
using UnityEngine.UI;

namespace testJava.script.model {
    public class UI {
        public UICtrl ctrl;

        public PlayerUI playerUI;

        public CardViewUI cardViewUI;
        public OrgUI orgUI;
        public MilitaryUI militaryUI;
        public CityCardUI cityCardUI;

        int cardType;
        public void viewBuildingCard (int cardType) {

            this.cardType = cardType;

            ctrl.maskBuildingImage.gameObject.SetActive (true);
            ctrl.maskBuildingImage.transform.SetAsLastSibling ();
            List<BuildingCard> buildingCards = U.cpUI.getBuildingCards (cardType);
            for (int i = 0; i < buildingCards.Count; i++) {
                BuildingCard card = buildingCards[i];
                card.ctrl.gameObject.transform.localPosition = new Vector3 (i * 100 - 200, 0, 0);
                card.ctrl.gameObject.transform.localScale = new Vector2 (3, 3);
                card.ctrl.gameObject.transform.SetAsLastSibling ();
            }
        }

        public void closeViewBuildingCard () {
            ctrl.maskBuildingImage.gameObject.SetActive (false);
            List<BuildingCard> buildingCards = U.cpUI.getBuildingCards (cardType);
            if (buildingCards == null) {
                return;
            }
            for (int i = 0; i < buildingCards.Count; i++) {
                BuildingCard card = buildingCards[i];
                U.hideCard (card.ctrl);
            }
        }
        public void closeAllView () {
            cardViewUI.closeView ();
            closeViewBuildingCard ();
        }
    }
}
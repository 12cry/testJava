using DG.Tweening;
using testCC.Assets.script;
using testJava.script.model;
using UnityEngine;

namespace testJava.script.model.card {
    public class CityCard : DiplomacyCard {
        public override void action2 () {

            U.currentCard = this;

            Vector3 cardViewPosition = new Vector3 (U.config.cardWidth * U.config.cardScale / 2 + 30, Screen.height / 2, 0);
            Vector3 cardViewScale = new Vector3 (U.config.cardScale, U.config.cardScale, 0);
            beforViewPosition = ctrl.transform.localPosition;
            beforViewScale = ctrl.transform.localScale;

            ctrl.transform.DOMove (cardViewPosition, U.config.cardMoveSpeed);
            ctrl.transform.DOScale (cardViewScale, U.config.cardMoveSpeed);

            U.ui.cityCardUI.view ();
        }
    }
}
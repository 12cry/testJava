using DG.Tweening;
using testCC.Assets.script;
using testJava.script.constant;
using UnityEngine;

namespace testJava.script.command {
    public class CardTakeCommand : Command {
        Card card;
        public CardTakeCommand (Card card) {
            this.card = card;
        }
        public override void undo () {
            U.g.handCardCtrls.Remove (card.ctrl);
            // card.ctrl.transform.DOLocalMove (new Vector3 (U.cardWidth / 2 + card.showIndex * U.cardWidth - Screen.width / 2, Screen.height / 2 - U.cardWidth / 2, 0), U.cardMoveSpeed);
            card.state = CardState.SHOWING;
            U.ui.actionUI.updateCivilRemainder (card.takeCivil);

            base.undo ();
        }
        public override void redo () {
            card.take ();

            base.redo ();
        }

    }
}
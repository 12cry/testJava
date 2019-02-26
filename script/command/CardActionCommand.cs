using DG.Tweening;
using testCC.Assets.script;
using testJava.script.constant;
using UnityEngine;

namespace testJava.script.command {
    public class CardActionCommand : Command {
        Card card;
        public CardActionCommand (Card card) {
            this.card = card;
        }
        public override void undo () {
            // card.undoAction ();

            base.undo ();
        }
        public override void redo () {
            card.action ();

            base.redo ();
        }

    }
}
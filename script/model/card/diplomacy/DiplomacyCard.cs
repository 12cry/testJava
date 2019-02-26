using testCC.Assets.script;
using testJava.script.constant;
using testJava.script.model;
using UnityEngine;

namespace testJava.script.model.card {
    public class DiplomacyCard : Card {

        public override void action () {
            U.ui.closeAllView ();
            U.hideCard (ctrl);

            int count = U.g.diplomacyPrepareCards.Count;
            if (count > 0) {
                int index = Random.Range (0, count);
                DiplomacyCard prepareCard = U.g.diplomacyPrepareCards[index];
                U.g.diplomacyPrepareCards.RemoveAt (index);
                prepareCard.action2 ();
            }

            U.g.diplomacyPrepareCards.Add (this);
            state = CardState.PREPARE;
            U.cpUI.actionUI.updateDiplomacyRemainder (-1);

        }
        public virtual void action2 () {

        }
    }
}
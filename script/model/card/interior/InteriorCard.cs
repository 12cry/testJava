using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using testCC.Assets.script;
using testCC.Assets.script.ctrl;
using testCC.Assets.script.model;
using testJava.script.command;
using testJava.script.constant;
using testJava.script.model;
using UnityEngine;
using UnityEngine.UI;

public class InteriorCard : Card {
    public int takeCivil = 1;

    public override void showViewButton () {
        int civilRemainder = U.ui.actionUI.civilRemainder;
        if (civilRemainder == 0) {
            return;
        }
        if (state == CardState.INROW) {
            if (civilRemainder >= takeCivil) {
                U.addAButton (0, string.Format ("take card"), delegate { take (); }, true);
                // U.showAButton (U.ui.cardViewUI.bTakeCard, 0);
            }
        } else if (state == CardState.INHAND) {
            if (getActionAble ()) {
                U.showAButton (U.ui.cardViewUI.bActionCard, 0);
            }
        } else if (state == CardState.END) {
            displayBuildButtons ();
        }
    }

    public void take () {
        U.g.interiorHandCardCtrls.Add (ctrl);
        ctrl.transform.DOMove (new Vector3 (U.config.cardWidth / 2 + U.g.interiorHandCardCtrls.Count * 20, U.config.cardHeight / 2, 0), U.config.cardMoveSpeed);
        ctrl.transform.DOScale (new Vector3 (1, 1, 0), U.config.cardMoveSpeed);

        state = CardState.INHAND;
        U.ui.actionUI.updateCivilRemainder (-this.takeCivil);
        U.ui.closeAllView ();
    }
    public override void action () {
        U.g.interiorHandCardCtrls.Remove (this.ctrl);
        U.g.interiorPassCardCtrls.Add (this.ctrl);
        U.hideCard (this.ctrl);

        state = CardState.END;
        U.ui.actionUI.updateCivilRemainder (-1);
    }
    // public void show () {
    //     state = CardState.SHOWING;
    // }

    public virtual void displayBuildButtons () { }

}
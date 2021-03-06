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
    public int takeInterior = 1;

    public override void showViewButton () {
        int interiorRemainder = U.cpUI.actionUI.interiorRemainder;
        if (interiorRemainder == 0) {
            return;
        }
        if (state == CardState.INROW) {
            if (interiorRemainder >= takeInterior) {
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
        U.cpUI.handCardUI.interiorHandCardCtrls.Add (ctrl);
        ctrl.transform.parent = U.cpUI.handCardUI.ctrl.transform;
        ctrl.transform.DOMove (new Vector3 (U.config.cardWidth / 2 + U.cpUI.handCardUI.interiorHandCardCtrls.Count * 20, U.config.cardHeight / 2, 0), U.config.cardMoveSpeed);
        ctrl.transform.DOScale (new Vector3 (1, 1, 0), U.config.cardMoveSpeed);

        state = CardState.INHAND;
        U.cpUI.actionUI.updateInteriorRemainder (-this.takeInterior);
        U.ui.closeAllView ();
    }
    public override void action () {
        U.cpUI.handCardUI.interiorHandCardCtrls.Remove (this.ctrl);
        U.g.interiorPassCardCtrls.Add (this.ctrl);
        U.hideCard (this.ctrl);

        state = CardState.END;
        U.cpUI.actionUI.updateInteriorRemainder (-1);
    }

    public virtual void displayBuildButtons () { }

}
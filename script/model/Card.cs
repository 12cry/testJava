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

public abstract class Card {
    public CardCtrl ctrl;

    public int id;
    public string cardName;
    public int age;
    public string desc;
    public CardType cardType;

    public CardState state = CardState.READY;
    public int takeCivil = 1;
    public int showIndex;

    public Vector3 beforViewPosition;
    public Dictionary<string, Text> textDic;

    UI ui = U.ui;

    public void init () {
        Text[] texts = ctrl.GetComponentsInChildren<Text> ();
        textDic = texts.ToDictionary (key => key.name, text => text);
    }
    public void view () {
        Vector3 cardViewPosition = new Vector3 (0, 0, 0);
        if (cardViewPosition == ctrl.transform.localPosition) {
            return;
        }
        beforViewPosition = ctrl.transform.localPosition;
        U.currentCard = this;
        ctrl.transform.DOLocalMove (cardViewPosition, U.cardMoveSpeed);

        ui.cardViewUI.view ();
        showViewButton ();
    }

    public void showViewButton () {
        int civilRemainder = U.ui.actionUI.civilRemainder;
        if (civilRemainder == 0) {
            return;
        }
        int index = 0;
        if (state == CardState.SHOWING) {
            if (civilRemainder >= takeCivil) {
                U.showAButton (U.ui.cardViewUI.ctrl.bTakeCard, index++);
            }
        } else if (state == CardState.TAKED) {
            if (actionAble ()) {
                U.showAButton (U.ui.cardViewUI.ctrl.bActionCard, index++);
            }
        } else if (state == CardState.ACTINGED) {
            displayActionButtons ();
        }
    }
    public void closeView () {
        ctrl.transform.DOLocalMove (beforViewPosition, U.cardMoveSpeed);
    }

    public void take () {
        U.g.handCardCtrls.Add (ctrl);
        ctrl.transform.DOLocalMove (new Vector3 (U.cardWidth / 2 - Screen.width / 2 + U.g.handCardCtrls.Count * 20, U.cardWidth / 2 - Screen.height / 2, 0), U.cardMoveSpeed);

        state = CardState.TAKED;
        ui.actionUI.updateCivilRemainder (-this.takeCivil);
    }
    public virtual void undoAction () {
        U.g.handCardCtrls.Add (ctrl);
        U.g.passCardCtrls.Remove (ctrl);
        ctrl.transform.DOLocalMove (new Vector3 (U.cardWidth / 2 - Screen.width / 2 + U.g.handCardCtrls.Count * 20, U.cardWidth / 2 - Screen.height / 2, 0), U.cardMoveSpeed);
        state = CardState.ACTINGED;
        U.ui.actionUI.updateCivilRemainder (1);
    }

    public virtual void action () {
        U.g.handCardCtrls.Remove (this.ctrl);
        U.g.passCardCtrls.Add (this.ctrl);
        U.hideCard (this.ctrl);

        state = CardState.ACTINGED;
        ui.actionUI.updateCivilRemainder (-1);
    }
    public virtual void show () {
        textDic["cardName"].text = cardName;
        state = CardState.SHOWING;
    }
    public virtual void displayActionButtons () { }
    public virtual bool actionAble () {
        return true;
    }
}
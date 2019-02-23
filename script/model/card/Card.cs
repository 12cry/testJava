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
    public string name;
    public int age;
    public string desc;
    public int type;

    public CardState state = CardState.START;
    public bool actionAble = true;

    public Vector3 beforViewPosition;
    public Vector3 beforViewScale;

    public virtual void init () {
        render ();
    }
    public void view () {

        if (U.ui.cardViewUI.state == ViewState.SHOW) {
            return;
        }

        Vector3 cardViewPosition = new Vector3 (U.config.cardWidth * U.config.cardScale / 2 + 30, Screen.height / 2, 0);
        Vector3 cardViewScale = new Vector3 (U.config.cardScale, U.config.cardScale, 0);
        beforViewPosition = ctrl.transform.localPosition;
        beforViewScale = ctrl.transform.localScale;

        U.currentCard = this;
        ctrl.transform.DOMove (cardViewPosition, U.config.cardMoveSpeed);
        ctrl.transform.DOScale (cardViewScale, U.config.cardMoveSpeed);

        U.ui.cardViewUI.view ();
        showViewButton ();
    }

    public virtual void showViewButton () {
        if (state == CardState.INHAND) {
            if (getActionAble ()) {
                U.showAButton (U.ui.cardViewUI.bActionCard, 0);
            }
        }
    }
    public void resetPosition () {
        ctrl.transform.DOLocalMove (beforViewPosition, U.config.cardMoveSpeed);
        ctrl.transform.DOScale (beforViewScale, U.config.cardMoveSpeed);
    }
    public virtual void action () { }
    public virtual void render () {
        ctrl.cardNameText.text = name;
    }

    public virtual bool getActionAble () {
        return actionAble;
    }
    public void setActionAble (bool actionAble) {
        this.actionAble = actionAble;
    }
    public virtual void undoAction () {
        U.g.interiorHandCardCtrls.Add (ctrl);
        U.g.interiorPassCardCtrls.Remove (ctrl);
        // ctrl.transform.DOLocalMove (new Vector3 (U.cardWidth / 2 - Screen.width / 2 + U.g.interiorHandCardCtrls.Count * 20, U.cardWidth / 2 - Screen.height / 2, 0), U.config.cardMoveSpeed);
        // state = CardState.ACTINGED;
        U.ui.actionUI.updateCivilRemainder (1);
    }

}
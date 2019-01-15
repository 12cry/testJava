using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using testCC.Assets.script;
using testCC.Assets.script.ctrl;
using testCC.Assets.script.model;
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
    public Cost actionCost;
    public Income actionIncome;
    public Cost buildCost;
    public Income buildIncome;
    public CardLevelType levelType;

    public CardState state = CardState.READY;
    public int takeCiv = 1;

    Tweener cardTween;
    UI ui = Utils.ui;

    public void init () {
        Text[] texts = ctrl.GetComponentsInChildren<Text> ();
        Dictionary<string, Text> textDic = texts.ToDictionary (key => key.name, text => text);
        textDic["cardName"].text = cardName;
        if (actionCost.science != 0)
            textDic["costScience"].text = actionCost.science.ToString ();
    }
    public void view () {
        Utils.currentCard = this;

        cardTween = ctrl.transform.DOMove (new Vector3 (Screen.width / 2, Screen.height / 2, 0), Utils.cardMoveSpeed).SetAutoKill (false);

        ui.cardViewUI.view ();
    }
    public void closeView () {
        if (cardTween.IsActive ()) {
            cardTween.Rewind ();
            cardTween.Kill ();
        }
    }
    public void take () {
        ctrl.transform.DOMove (new Vector3 (Utils.cardWidth / 2 + Utils.handCardCtrls.Count * 20, Utils.cardWidth / 2, 0 + Utils.handCardCtrls.Count), Utils.cardMoveSpeed);
        state = CardState.TAKED;
        Utils.handCardCtrls.Add (ctrl);
        ui.resourceUI.updateCiv (this.takeCiv);
    }

    public virtual void action () {
        state = CardState.ACTINGED;
        ui.resourceUI.updateCiv (1);
        Utils.handCardCtrls.Remove (this.ctrl);
        Utils.passCardCtrls.Add (this.ctrl);
        Utils.hideCard (this.ctrl);
    }

    // public abstract void action ();
}
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
    public int takeCivil = 1;

    Vector3 cardPositionTemp;

    UI ui = Utils.ui;

    public void init () {
        Text[] texts = ctrl.GetComponentsInChildren<Text> ();
        Dictionary<string, Text> textDic = texts.ToDictionary (key => key.name, text => text);
        textDic["cardName"].text = cardName;
        if (actionCost.science != 0)
            textDic["costScience"].text = actionCost.science.ToString ();
    }
    public void view () {
        Vector3 cardViewPosition = new Vector3 (0, 0, 0);
        if (cardViewPosition == ctrl.transform.localPosition) {
            return;
        }

        Utils.currentCard = this;
        cardPositionTemp = ctrl.transform.localPosition;
        ctrl.transform.DOLocalMove (cardViewPosition, Utils.cardMoveSpeed);

        ui.cardViewUI.view ();
    }
    public void closeView () {
        ctrl.transform.DOLocalMove (cardPositionTemp, Utils.cardMoveSpeed);
    }
    public void take () {
        ctrl.transform.DOLocalMove (new Vector3 (Utils.cardWidth / 2 - Screen.width / 2 + Utils.handCardCtrls.Count * 20, Utils.cardWidth / 2 - Screen.height / 2, 0 + Utils.handCardCtrls.Count), Utils.cardMoveSpeed);
        Utils.handCardCtrls.Add (ctrl);

        state = CardState.TAKED;
        ui.actionUI.updateCivilRemainder (-this.takeCivil);
    }

    public virtual void action () {
        Utils.handCardCtrls.Remove (this.ctrl);
        Utils.passCardCtrls.Add (this.ctrl);
        Utils.hideCard (this.ctrl);

        state = CardState.ACTINGED;
        ui.actionUI.updateCivilRemainder (-1);
    }
}
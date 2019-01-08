using DG.Tweening;
using testCC.Assets.script;
using testCC.Assets.script.ctrl;
using testCC.Assets.script.model;
using testJava.script.model;
using UnityEngine;
using UnityEngine.UI;

public abstract class Card {
    public CardCtrl ctrl;

    public int id;
    public string cardName;
    public int age;
    public Cost cost;
    public Income income;

    public bool takeable;
    public bool actionable;
    public bool taked = false;
    public int takeCiv = 1;

    Tweener cardTween;
    UI ui = Utils.ui;
    public void view () {
        Utils.currentCard = this;

        cardTween = ctrl.transform.DOMove (new Vector3 (Screen.width / 2, Screen.height / 2, 0), Utils.cardMoveSpeed).SetAutoKill (false);

        ui.cardViewUI.buttonAble (takeable, actionable);
        ui.cardViewUI.showView ();
    }
    public void closeView () {
        if (cardTween.IsActive ()) {
            cardTween.Rewind ();
            cardTween.Kill ();
        }
        ui.cardViewUI.hideView ();
    }
    public void take () {
        ctrl.transform.DOMove (new Vector3 (Utils.cardWidth / 2 + Utils.handCardCtrls.Count * 20, Utils.cardWidth / 2, 0 + Utils.handCardCtrls.Count), Utils.cardMoveSpeed);
        actionable = true;
        takeable = false;
        taked = true;
        Utils.handCardCtrls.Add (ctrl);
        ui.resourceUI.updateCiv (this.takeCiv);

        ui.cardViewUI.hideView ();
    }
    public void updateResource () {
        ui.resourceUI.updateScience (-cost.science);
        ui.resourceUI.updateFoodIncrement (income.food);
        ui.resourceUI.updateCapacityIncrement (income.capacity);
        // Utils.resource.updateScienceIncrement (output[2]);
        // Utils.resource.updateCultureIncrement (output[3]);
    }

    public void afterAction () {
        ui.resourceUI.updateCiv (1);
        ui.cardViewUI.hideView ();
        Utils.handCardCtrls.Remove (this.ctrl);
        Object.Destroy (this.ctrl.gameObject);
    }
    public abstract void action ();
}
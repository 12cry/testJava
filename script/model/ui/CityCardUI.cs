using System.Collections.Generic;
using testCC.Assets.script;
using testCC.Assets.script.model;
using testJava.script.constant;
using testJava.script.ctrl.ui;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace testJava.script.model.ui {
    public class CityCardUI {
        public CityCardUICtrl ctrl;
        public void view () {

            ctrl.gameObject.SetActive (true);

        }
    }
}
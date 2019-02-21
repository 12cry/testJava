using System.Collections.Generic;
using testCC.Assets.script;
using testJava.script.model.ui;
using TMPro;
using UnityEngine;

namespace testJava.script.ctrl.ui {
    public class StatisticUICtrl : MonoBehaviour {
        public StatisticUI ui;

        public TextMeshProUGUI text;
        public void init () {
            ui = new StatisticUI ();
            ui.ctrl = this;
            ui.init ();

        }

    }
}
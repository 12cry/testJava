using System.Collections.Generic;
using testCC.Assets.script;
using testJava.script.ctrl.ui;

namespace testJava.script.model.ui {
    public class HandCardUI {
        public HandCardUICtrl ctrl;

        public List<CardCtrl> interiorHandCardCtrls = new List<CardCtrl> ();
        public List<CardCtrl> diplomacyHandCardCtrls = new List<CardCtrl> ();
    }
}
using testCC.Assets.script;
using testJava.script.constant;
using UnityEngine;

public class CardBuild : Card {

    public override void action () {
        base.action ();
        Utils.world.build (this);
    }
}
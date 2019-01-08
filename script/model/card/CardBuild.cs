using testCC.Assets.script;
using UnityEngine;

public class CardBuild : Card {

    public override void action () {
        Debug.Log ("action---");
        this.updateResource ();
        Utils.world.build (this.id);
        this.afterAction ();
    }
}
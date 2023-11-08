using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Trap : Interactable
{

    protected override void Awake() {
        base.Awake();
    }

    public override void Interact(GameObject actor) {
        if (actor.tag != "MiniBoss") return;


    }
}

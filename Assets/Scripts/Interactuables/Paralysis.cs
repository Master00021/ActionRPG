using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Paralysis : Interactable
{
    
    protected override void Awake() {
        base.Awake();
    }

    public override void Interact(GameObject actor) {
        //if (actor.GetComponent<MiniBoss>().rage) return;

        
    }
}

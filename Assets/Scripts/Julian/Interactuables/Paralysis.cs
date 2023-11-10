using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

internal sealed class Paralysis : Interactable
{
    internal static event Action OnActorDetected;
    
    protected override void Awake() {
        base.Awake();
    }

    internal override void Interact(GameObject actor) {
        //if (actor.GetComponent<MiniBoss>().Rage) return;

        OnActorDetected?.Invoke();
    }

}

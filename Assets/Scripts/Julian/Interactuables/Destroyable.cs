using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

internal sealed class Destroyable : Interactable
{
    internal static event Action OnDamageDetected;
    
    protected override void Awake() {
        base.Awake();
    }

    internal override void Interact(GameObject actor) {
        OnDamageDetected?.Invoke();
    }
    
}

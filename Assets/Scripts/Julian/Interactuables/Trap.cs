using System;
using UnityEngine;

internal sealed class Trap : Interactable
{
    internal static event Action OnMiniBossDetected;

    protected override void Awake() {
        base.Awake();
    }

    internal override void Interact(GameObject actor) {
        if (actor.tag != "MiniBoss") return;

        OnMiniBossDetected?.Invoke();
    }

}

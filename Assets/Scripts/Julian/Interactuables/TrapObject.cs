using UnityEngine;
using System;

internal sealed class TrapObject : Interactable {

    internal static event Action<GameObject, GameObject> OnActorDetected;

    protected override void Awake() {
        base.Awake();
    }

    internal override void Interact(GameObject actor) {
        if (actor.tag != "MiniBoss") return;
        OnActorDetected?.Invoke(actor, gameObject);
    }

}

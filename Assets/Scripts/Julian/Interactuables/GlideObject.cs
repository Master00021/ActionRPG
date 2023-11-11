using System;
using UnityEngine;

internal sealed class GlideObject : Interactable
{
    internal static event Action OnPlayerEntry;
    internal static event Action OnPlayerExit;
    
    protected override void Awake() {
        base.Awake();
    }

    internal override void Interact(GameObject actor) {
        if (actor.tag != "Player") return;

        //PlayerGliderInput.AllowInput = false;
        OnPlayerEntry?.Invoke();
    }

    private void OnTriggerExit(Collider actor) {
        if (actor.tag != "Player") return;

        //PlayerGliderInput.AllowInput = true;
        OnPlayerExit?.Invoke();
    }

}

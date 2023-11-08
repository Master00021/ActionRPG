using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected BoxCollider MyCollider { get; private set; }    
    protected string Name { get; set; }

    protected virtual void Awake() {
        MyCollider = GetComponent<BoxCollider>();
        this.Name = gameObject.name;
    }

    protected void OnTriggerEnter(Collider actor) {
        Interact(actor.gameObject);
    }

    public abstract void Interact(GameObject actor);
}

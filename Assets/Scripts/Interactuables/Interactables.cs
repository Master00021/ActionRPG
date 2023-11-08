using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public abstract class Interactables : MonoBehaviour
{
    protected BoxCollider _myCollider;    
    protected string _name { get; set; }

    protected virtual void Awake() {
        _myCollider = GetComponent<BoxCollider>();
        this._name = gameObject.name;
    }

    public abstract void Interact();
}

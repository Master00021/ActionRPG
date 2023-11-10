using UnityEngine;

internal abstract class Interactable : MonoBehaviour
{
    protected BoxCollider MyCollider { get; private set; }    
    protected string Name { get; set; }

    protected virtual void Awake() {
        MyCollider = GetComponent<BoxCollider>();
        Name = gameObject.name;
    }

    protected void OnTriggerEnter(Collider actor) {
        Interact(actor.gameObject);
    }

    internal abstract void Interact(GameObject actor);
}

using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Floating : Interactable
{

    [SerializeField] private float _explosionForce;

    protected override void Awake() {
        base.Awake();
    }

    public override void Interact(GameObject actor) {
        if (actor.tag != "Player") return;

        // El player tiene que subir rapidamente y luego quedar flotando, "planeando"

        //actor.GetComponent<Player>().AllowInput = false;

        var rigidBody = actor.GetComponent<Rigidbody>();
        var _direction = gameObject.transform.up;

        FloatingController.OnPlayerMovement += FloatControl;

        rigidBody.AddForce(_direction * _explosionForce, ForceMode.Impulse);

        if (rigidBody.velocity.y < 0.0f) {
            FloatControl();
        }
    }

    private void FloatControl(float horizontal, float vertical) {

    }
}

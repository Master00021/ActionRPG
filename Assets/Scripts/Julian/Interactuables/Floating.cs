using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

internal sealed class Floating : Interactable
{

    [SerializeField] private float _explosionForce;

    //private Character
    private Rigidbody _playerRigidBody;
    private Vector3 _playerDirection;

    protected override void Awake() {
        base.Awake();
    }

    internal override void Interact(GameObject actor) {
        if (actor.tag != "Player") return;

        // El player tiene que subir rapidamente y luego quedar flotando, "planeando"

        //actor.GetComponent<Player>().AllowInput = false;

        _playerRigidBody = actor.GetComponent<Rigidbody>();
        _playerDirection = gameObject.transform.up;

        _playerRigidBody.AddForce(_playerDirection * _explosionForce, ForceMode.Impulse);

        FloatingController.OnPlayerMovement += TakeControlOverPlayer;      


    }

    private void TakeControlOverPlayer(float horizontal, float vertical) {

        print(_playerRigidBody.velocity.y);

        if (_playerRigidBody.velocity.y < 0.0f) {
            // PLANEAR

            print("Estoy planeando");

            _playerRigidBody.mass /= 2;
        }

    }
}

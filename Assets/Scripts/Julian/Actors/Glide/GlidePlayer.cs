using System.Collections;
using UnityEngine;

internal sealed class GlidePlayer : MonoBehaviour, IGlide {

    [SerializeField] private GlideConfiguration _glideConfiguration;
    [SerializeField] private Rigidbody _playerRigidBody;
    [SerializeField] private LayerMask _groundLayerMask;


    public void Impulse(Transform maxHeight) {
        _playerRigidBody.drag = 0.0f;

        StartCoroutine(CO_WaitToMaxHeight());

        IEnumerator CO_WaitToMaxHeight() {
            while (_playerRigidBody.transform.position.y < maxHeight.position.y) {
                if (_playerRigidBody.velocity.y > 15.0f) yield return null;
                _playerRigidBody.AddForce(_playerRigidBody.transform.up * _glideConfiguration.ImpulseForce * Time.deltaTime, ForceMode.Impulse);
                yield return null; 
            }
            Glide();
        } 
    }

    public void Glide(){
        _playerRigidBody.drag = 5.0f;

        StartCoroutine(CO_WaitForGrounded());

        IEnumerator CO_WaitForGrounded() {
            while (!Physics.Raycast(transform.position, -transform.up, 2.0f, _groundLayerMask)) {
                yield return null;  
            }
            _playerRigidBody.drag = 0.0f;
        } 
    }

}

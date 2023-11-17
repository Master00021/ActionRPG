using System.Collections;
using UnityEngine;

internal sealed class GlideActor : MonoBehaviour, IGlide {

    [SerializeField] private LayerMask _groundLayerMask;

    public void Impulse(GlideConfiguration configuration, Rigidbody rigidbody, Transform maxHeight) {
        rigidbody.drag = 0.0f;

        StartCoroutine(CO_WaitToMaxHeight());

        IEnumerator CO_WaitToMaxHeight() {
            while (rigidbody.transform.position.y < maxHeight.position.y) {
                if (rigidbody.velocity.y > 15.0f) yield return null;
                rigidbody.AddForce(rigidbody.transform.up * configuration.ImpulseForce * Time.deltaTime, ForceMode.Impulse);
                yield return null; 
            }
            Glide(rigidbody);
        } 
    }

    public void Glide(Rigidbody rigidbody){
        rigidbody.drag = 5.0f;

        StartCoroutine(CO_WaitForGrounded());

        IEnumerator CO_WaitForGrounded() {
            while (!Physics.Raycast(transform.position, -transform.up, 2.0f, _groundLayerMask)) {
                yield return null;  
            }
            rigidbody.drag = 0.0f;
        } 
    }

}

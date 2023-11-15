using System.Collections;
using UnityEngine;

internal sealed class TrapActor : MonoBehaviour, ITrap {
    
    [SerializeField] private TrapConfiguration _trapConfiguration;
    [SerializeField] private Rigidbody _rigidBody;

    public void Stun(float stunTime) {
        _rigidBody.isKinematic = true;

        StartCoroutine(CO_WaitForStunTimeEnd());

        IEnumerator CO_WaitForStunTimeEnd() {
            while (stunTime > 0.0f) {
                stunTime -= Time.deltaTime;
                yield return null;
            }
            _rigidBody.isKinematic = false;
        }
    }

    public void Reactivate(GameObject trap, float timeToReactivate) {

        StartCoroutine(CO_WaitToReactivate());

        IEnumerator CO_WaitToReactivate() {
            while (timeToReactivate > 0.0f) {
                timeToReactivate -= Time.deltaTime;
                yield return null;
            }
            trap.SetActive(true);
        }
    }
    
}

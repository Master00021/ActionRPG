using System.Collections;
using UnityEngine;

internal sealed class TrapActor : MonoBehaviour, ITrap {
    
    public void Stun(Rigidbody rigidbody, float stunTime) {
        rigidbody.isKinematic = true;

        StartCoroutine(CO_WaitForStunTimeEnd());

        IEnumerator CO_WaitForStunTimeEnd() {
            while (stunTime > 0.0f) {
                stunTime -= Time.deltaTime;
                yield return null;
            }
            rigidbody.isKinematic = false;
        }
    }

    public void Reactivate(TrapConfiguration configuration, GameObject trap, float timeToReactivate) {

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

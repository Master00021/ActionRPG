using UnityEngine;

internal sealed class TrapObject : MonoBehaviour {

    [SerializeField] private TrapConfiguration _trapConfiguration;

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("MiniBoss")) return;

        var rigidbody = other.GetComponent<Rigidbody>();

        other.TryGetComponent<ITrap>(out var trapActor);
        
        trapActor.Stun(rigidbody, _trapConfiguration.StunTime);
        trapActor.Reactivate(_trapConfiguration, gameObject, _trapConfiguration.TimeToReactivate);
        gameObject.SetActive(false);
    }

}

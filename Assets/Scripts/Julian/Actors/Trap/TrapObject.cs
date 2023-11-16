using UnityEngine;

internal sealed class TrapObject : MonoBehaviour {

    [SerializeField] private TrapConfiguration _trapConfiguration;

    private void OnTriggerEnter(Collider other) {
        other.TryGetComponent<ITrap>(out var trapActor);
        trapActor.Stun(_trapConfiguration.BossStunTime);
        trapActor.Reactivate(gameObject, _trapConfiguration.TimeToReactivate);
        gameObject.SetActive(false);
    }

}

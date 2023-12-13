using UnityEngine;

internal sealed class GlideObject : MonoBehaviour {

    [SerializeField] private GlideConfiguration configuration;
    [SerializeField] private Transform _maxHeight;

    private Rigidbody _rigidBody;

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;
        print(other.name);
        _rigidBody = other.GetComponent<Rigidbody>();
        other.TryGetComponent<IGlide>(out var glidePlayer);
        glidePlayer.Impulse(configuration, _rigidBody, _maxHeight);
    }

}

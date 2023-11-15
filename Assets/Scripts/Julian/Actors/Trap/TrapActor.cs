using UnityEngine;

internal sealed class TrapActor : MonoBehaviour {
    
    [SerializeField] private bool _playOnStart;
    [SerializeField] private TrapConfiguration _trapConfiguration;
    [SerializeField] private Trap _trap;

    private Rigidbody _rigidBody;
    private bool _isPlaying;

    private void Awake() {
        if (_playOnStart) {
            Play();
        }
    }

    private void OnDisable() {
        _trap?.Disable();
    }

    private void Update() {
        _trap.Explode();
        _trap.BossStun();
        _trap.ReactivateTrap();
    }

    private void Play() {
        _rigidBody = GetComponent<Rigidbody>();
        _trap = new(_trapConfiguration, _rigidBody);   
    }
    
}

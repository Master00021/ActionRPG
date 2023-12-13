using UnityEngine;

internal sealed class ParalysisObject : MonoBehaviour {

    [SerializeField] private ParalysisConfiguration configuration;
    [SerializeField] private Vector3 _paralysisArea;

    private float _timeToDestroy;
    private bool _activated;

    private void Awake() {
        _timeToDestroy = configuration.TimeToStop;
    }

    private void Update() {
        _timeToDestroy -= Time.deltaTime;
        if (_timeToDestroy < 0.0f) {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if (!other.TryGetComponent<IParalyze>(out var paralizable)) return;
        
        if (other.CompareTag("Attack") && !_activated) {
            _activated = true;
            print(other.name);
            var rigidbody = other.GetComponent<Rigidbody>();
            gameObject.GetComponent<BoxCollider>().size = _paralysisArea;

            other.TryGetComponent<IParalyze>(out var actor);
            actor.Paralyze(configuration, rigidbody, configuration.TimeToStop, _activated);
        }

        if (!other.CompareTag("Attack") && _activated) {
            var rigidbody = other.GetComponent<Rigidbody>();
            print(other.name);
            other.TryGetComponent<IParalyze>(out var actor);
            actor.Paralyze(configuration, rigidbody, configuration.TimeToStop, _activated);
        }
    }

}

using UnityEngine;

internal sealed class ParalysisObject : MonoBehaviour {

    [SerializeField] private ParalysisConfiguration _paralysisConfiguration;

    private float _timeForBackToNormal;

    private void Awake() {
        _timeForBackToNormal = _paralysisConfiguration.TimeToEndParalysis;
        _paralysisConfiguration.Activated = false;
    }

    private void Update() {
        _timeForBackToNormal -= Time.deltaTime;
        if (_timeForBackToNormal < 0.0f) {
            gameObject.GetComponent<BoxCollider>().size = new Vector3(1.0f, 1.0f, 1.0f);
            _timeForBackToNormal = _paralysisConfiguration.TimeToEndParalysis;
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Attack") || _paralysisConfiguration.Activated) return; // Condicional rota
        _paralysisConfiguration.Activated = true;

        print(other.name);

        gameObject.GetComponent<BoxCollider>().size = new Vector3(5.0f, 5.0f, 5.0f);

        other.TryGetComponent<IParalyze>(out var paralyzableActor);
        paralyzableActor.Paralyze(_paralysisConfiguration.TimeToEndParalysis);
        paralyzableActor.Reactivate(_paralysisConfiguration, _paralysisConfiguration.TimeToReactivate);
    }

}

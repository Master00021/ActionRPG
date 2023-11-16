using System.Collections;
using UnityEngine;

internal sealed class Paralysis : MonoBehaviour, IParalyze {

    [SerializeField] private Rigidbody _rigidbody;
    
    public void Paralyze(float timeToEndParalysis) {
        
        StartCoroutine(CO_TimeToEndParalysis());

        IEnumerator CO_TimeToEndParalysis() {
            _rigidbody.isKinematic = true;
            while (timeToEndParalysis > 0.0f) {
                timeToEndParalysis -= Time.deltaTime;
                yield return null;
            }
            _rigidbody.isKinematic = false;
        }
    }

    public void Reactivate(ParalysisConfiguration paralysisConfiguration, float timeToReactivate) {

        StartCoroutine(CO_TimeToReactivate());

        IEnumerator CO_TimeToReactivate() {
            while (timeToReactivate > 0.0f) {
                timeToReactivate -= Time.deltaTime;
                yield return null;
            }
            paralysisConfiguration.Activated = false;
        }
    }
    
}

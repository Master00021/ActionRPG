using System.Collections;
using UnityEngine;

internal sealed class Paralysis : MonoBehaviour, IParalyze {

    [SerializeField] private Rigidbody _rigidbody;
    
    public void Paralyze(ParalysisConfiguration configuration, float timeToStop, bool activated) {
        
        StartCoroutine(CO_TimeToEndParalysis());

        IEnumerator CO_TimeToEndParalysis() {
            _rigidbody.isKinematic = true;
            while (timeToStop > 0.0f) {
                timeToStop -= Time.deltaTime;
                yield return null;
            }
            _rigidbody.isKinematic = false;
            activated = false;
        }
        
    }
    
}

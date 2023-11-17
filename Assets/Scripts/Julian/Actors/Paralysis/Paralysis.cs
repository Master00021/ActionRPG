using System.Collections;
using UnityEngine;

internal sealed class Paralysis : MonoBehaviour, IParalyze {
    
    public void Paralyze(ParalysisConfiguration configuration, Rigidbody rigidbody, float timeToStop, bool activated) {
        
        StartCoroutine(CO_TimeToEndParalysis());

        IEnumerator CO_TimeToEndParalysis() {
            rigidbody.isKinematic = true;
            while (timeToStop > 0.0f) {
                timeToStop -= Time.deltaTime;
                yield return null;
            }
            rigidbody.isKinematic = false;
            activated = false;
        }
        
    }
    
}

using UnityEngine;

internal interface IParalyze {
    
    public void Paralyze(ParalysisConfiguration configuration, Rigidbody rigidbody, float timeToStop, bool activated);
}

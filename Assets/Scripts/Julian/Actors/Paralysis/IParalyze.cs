using UnityEngine;

internal interface IParalyze {
    
    public void Paralyze(ParalysisConfiguration configuration, float timeToStop, bool activated);
}

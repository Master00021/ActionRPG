using UnityEngine;

internal interface IParalyze {
    
    public void Paralyze(float timeToEndParalysis);
    public void Reactivate(ParalysisConfiguration paralysisConfiguration, float timeToReactivate);
}

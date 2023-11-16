using UnityEngine;

[CreateAssetMenu]
internal sealed class ParalysisConfiguration : ScriptableObject {

    public float TimeToEndParalysis;
    public float TimeToReactivate;
    public bool Activated;

}

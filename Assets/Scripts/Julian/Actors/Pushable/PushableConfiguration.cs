using UnityEngine;

[CreateAssetMenu]
public sealed class PushableConfiguration : ScriptableObject {
    
    public float TimeToAccumulate;
    public float ForwardForce;
    public float RiseForce;
    public int HitsLimit;

}

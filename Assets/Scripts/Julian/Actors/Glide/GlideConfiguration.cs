using UnityEngine;

[CreateAssetMenu]
internal sealed class GlideConfiguration : ScriptableObject {
    
    public float ImpulseForce;
    public float GlideSpeed;
    public float GravityForce;
    public bool GlidePlayer;
    public bool ImpulsePlayer;    
}

using UnityEngine;

[CreateAssetMenu]
internal sealed class TrapConfiguration : ScriptableObject {
    
    public float ExplosionRadius;
    public float ExplosionOffset;
    public float UpWardsModifier;
    public float DeactivateTime;
    public float ExplosionForce;
    public float BossStunTime;

}

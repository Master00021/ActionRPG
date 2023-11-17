using UnityEngine;

internal sealed class DamageableActor : MonoBehaviour, IDamageable
{

    [SerializeField] private float _health;

    private void Update() {
        if (_health <= 0.0f) Destroy(gameObject);
    }
    
    public void Damage(float damage) {
        _health -= damage;
    }

}

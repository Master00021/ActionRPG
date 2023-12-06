using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGeneral : MonoBehaviour
{
    public float Damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var Damageable))
        {
            Damageable.Damage(Damage);
        }
    }
}

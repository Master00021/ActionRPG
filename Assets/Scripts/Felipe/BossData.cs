using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class BossData : ScriptableObject
{
    public float health;
    public float healrecoverispeed;
    public float stamina;
    public float staminarecoverispeed;

    public bool invulnerable;
    public bool Inrage;
    
}

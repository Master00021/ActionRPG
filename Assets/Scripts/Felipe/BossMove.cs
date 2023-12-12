using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class BossMove : MonoBehaviour
{
    public BossData Bossdata;
    public Animator anim;
    private Rigidbody rigi;
    public Transform targetPosition;
    
    private void Awake()
    {
        rigi = GetComponent<Rigidbody>();
    }
    public void Move()
    {
        var direction = targetPosition.position - transform.position;
        direction.y = 0.0f;
        rigi.AddForce(direction * Bossdata.Speed * Time.deltaTime,ForceMode.Force);
        anim.CrossFade("walk_forward", 0.1f);
    }
    public void ReturnSpawn()
    {

    }
   
}

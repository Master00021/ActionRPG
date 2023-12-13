using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BossMove : MonoBehaviour
{
    public BossData Bossdata;
    public Animator anim;
    public Transform target;
    public Transform Spawn;

    private bool InSpawn;
    private bool IsReturning;

    private void OnTriggerEnter(Collider other) {
        if (Bossdata.Disabled) return;
        if (other.CompareTag("Spawn")) {
            InSpawn = true;
            Bossdata.IsWalking = false;
        }
    }

    public void Move()
    {
        if (Bossdata.Disabled || IsReturning == true) return;
        if (Bossdata.IsAttacking == true || Bossdata.Isfallen == true || Bossdata.IsTired == true) return;
        var direction = target.position - transform.position;
        float angleVision = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, angleVision, 0.0f));
        transform.Translate(Vector3.forward * Bossdata.Speed * Time.deltaTime);

        InSpawn = false;

        if (Bossdata.IsWalking == false) {
            anim.CrossFade("walk_forward", 0.1f);
            Bossdata.IsWalking = true;
        }
    }
    public void ReturnSpawn()
    {
        if (Bossdata.Disabled) return;
        IsReturning = true;
        anim.CrossFade("walk_forward", 0.1f);
        StartCoroutine(CO_ReturnToSpawn());        
    }

    private IEnumerator CO_ReturnToSpawn() {
        while (InSpawn == false) {
            var direction = Spawn.position - transform.position;
            float angleVision = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, angleVision, 0.0f));
            transform.Translate(Vector3.forward * Bossdata.Speed * Time.deltaTime);
            yield return null;
        }
        anim.CrossFade("idle", 0.1f);
        IsReturning = false;
    }
   
}

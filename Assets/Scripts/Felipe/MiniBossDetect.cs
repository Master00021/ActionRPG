using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossDetect : MonoBehaviour
{
    public MiniBossData MiniData;
    public MiniBossPatron MiniPatron;
    public Animator anim;
    
    public float RangeAlert;
    public bool StayAlert;
    
    public LayerMask LayerPlayer;
    
    void Update()
    {
        StayAlert = Physics.CheckSphere(transform.position, RangeAlert, LayerPlayer);
        DetectPlayer();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, RangeAlert);
    }
    private void DetectPlayer()
    {
        if(StayAlert == true)
        {
            anim.SetBool("SleepEnd", true);
        }
    }
}

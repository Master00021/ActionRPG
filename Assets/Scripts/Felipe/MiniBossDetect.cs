using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossDetect : MonoBehaviour
{
    public Transform Minibos;
    public GameObject ObjetcFind;
    public MiniBossData MiniData;
    public MiniBossPatron MiniPatron;
    public Animator anim;
    
    public float RangeAlert;
    public float AnimationDelay;
    public bool StayAlert;
    public bool Awaken;

    
    public LayerMask LayerPlayer;
    public LayerMask LayerEnemy;
    public SphereCollider SphereCollider;
    void Update()
    {
        Awaken = Physics.CheckSphere(transform.position, RangeAlert, LayerPlayer);
        if(MiniData.isEnraged == true) 
        {
            StayAlert = Physics.CheckSphere(transform.position, RangeAlert, LayerEnemy);          
        }
        DetectPlayer();
        Runtowars();
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, RangeAlert);
    }
    private void Awake()
    {
        SphereCollider.enabled = false;
    }
    private void DetectPlayer()
    {
        if(Awaken == true)
        {           
            anim.SetBool("SleepEnd", true);
            if (AnimationDelay <= 0)
            {
                SphereCollider.enabled = true;
                StayAlert = true;
            }
            else
                AnimationDelay -= Time.deltaTime;

        }
        if (StayAlert == true)
        {
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        ObjetcFind = other.gameObject;
        if(other.gameObject.layer == LayerPlayer || other.gameObject.layer == LayerEnemy)
        {
           
        }
       
    }
    private void Runtowars()
    {
        if (ObjetcFind == null)
        {
            return;

        }
        if (MiniData.CanAttack == false && MiniData.ReturnSpawn == false)
        {
            Minibos.position = Vector3.MoveTowards(Minibos.position, ObjetcFind.transform.position, MiniData.Velocity *Time.deltaTime);
            anim.SetFloat("Walk", 1);
            MiniData.InSpawn = false;
            
        }
        
    }

}

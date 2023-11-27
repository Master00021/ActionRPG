using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossAttack : MonoBehaviour
{
    public MiniBossData MiniData;
    public MiniBossPatron MiniPatron;
    public MiniBossDetect MiniDetect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && MiniData.CanAttack == false)
        {
            MiniData.CanAttack = true;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && MiniData.CanAttack == true)
        {
            MiniData.CanAttack = false;
            
        }
    }
}

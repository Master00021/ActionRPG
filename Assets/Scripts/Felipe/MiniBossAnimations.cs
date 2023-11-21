using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class MiniBossAnimations : MonoBehaviour
{
    public MiniBossData MiniBoss;
    public MiniBossPatron MiniPatron;
    public Animator Anim;
    void Start()
    {
        Anim.SetBool("Hit1", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

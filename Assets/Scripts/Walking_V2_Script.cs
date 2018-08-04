using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking_V2_Script : MonoBehaviour {

    Animator anim;
  
    int walkStateHash = Animator.StringToHash("Base Layer.Walk");


    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        float move = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", move);

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        
    }
}
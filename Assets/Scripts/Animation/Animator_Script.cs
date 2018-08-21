using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Script : MonoBehaviour {

    Animator anim;

    int bored_idealHash = Animator.StringToHash("Bored_Ideal");
    int normal_idealHash = Animator.StringToHash("Normal_Ideal");
    int walkHash = Animator.StringToHash("Walk");
    int battleHash = Animator.StringToHash("Battle");
    int winHash = Animator.StringToHash("Win");
    int loseHash = Animator.StringToHash("Lose");

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

       /* if ()
        {
            anim.SetTrigger(bored_idealHash);
        }*/

        if (PlayerController.instance.normal_ideal_bool == true)
        {
            anim.SetTrigger(normal_idealHash);
        }

        if (PlayerController.instance.walkingBool == true)
        {
            anim.SetTrigger(walkHash);
        }

        /*if ()
        {
            anim.SetTrigger(battleHash);
        }

        if ()
        {
            anim.SetTrigger(winHash);
        }

        if ()
        {
            anim.SetTrigger(loseHash);
        }*/
    }
}

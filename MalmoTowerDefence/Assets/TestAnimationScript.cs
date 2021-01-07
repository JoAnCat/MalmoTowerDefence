using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimationScript : MonoBehaviour
{
    private Animator animator;
    private bool isWalking;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            isWalking = true;
            animator.SetBool("isWalking", true);
            print("TestAnimationScript: w was pressed");
        }
        else if (isWalking == true && Input.GetKey(KeyCode.W) == false)
        {
            isWalking = false;
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("isAttacking");
            print("TestAnimationScript: A was pressed");
        }
        
        
            
        //print($"TestAnimationScript: iswalking {animator.GetBool("isWalking").ToString()}");
        //print($"TestAnimationScript: isAttacking {animator.GetBool("isAttacking").ToString()}");
    }
}

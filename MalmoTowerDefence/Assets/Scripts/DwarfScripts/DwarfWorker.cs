using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfWorker : Dwarf
{
    private Animator anim;
    private float bag = 0f, fullBag = 100f;
    [SerializeField] private float diamondsPerSwing = 25f;
    private void Start()
    {
        InvokeRepeating("WorkerActions", 1, 4);
        anim = GetComponent<Animator>();
    }

    private void WorkerActions()
    {
        print("in worker actions");
        if (wasAttacked)
            DwarfAttacked();
        else if(fullBag <= bag)
            WaitForBagCollection();
        else
        {
            MineGround();
            print($"mining ground bag {bag.ToString()}");
        }
    }

    private void DwarfAttacked()
    {
        if (isDead)
            DwarfDeath();
        else
            anim.SetTrigger("attacked");
        wasAttacked = false;
    }

    private void WaitForBagCollection()
    {
        anim.SetBool("hasDiamonds", true);
        bag -= 100;
        // player collect set bag to 0 or -100 and has diamonds to false
    }

    private void MineGround()
    {
        anim.SetTrigger("working");
        bag += diamondsPerSwing;
    }

    private void DwarfDeath()
    {
        anim.SetBool("isDead", true);
        //enque till pool och sätta tile till empty
    }

    protected override void ResetValues()
    {
        // if dead reset and send to queue;
    }
}

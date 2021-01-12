using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dwarf : MonoBehaviour
{
    protected Transform thisTransform;
    //protected DwarfData myData;
    protected float currentHealth;
    protected bool wasAttacked;
    protected bool isDead;

    public int row;

    public int column;


    public void TakeDamage(int DAMAGE)
    {
        if(DAMAGE <= 0)
            Debug.Log("dwarf: Damage can't be zero or less");
        currentHealth -= DAMAGE;
        if (currentHealth < 0)
        {
            isDead = true;
            
        }
            
        wasAttacked = true;
        print($"dwarf took damage: Damage taken {DAMAGE} current health {currentHealth}");
    }



    public abstract void ResetValues();

}

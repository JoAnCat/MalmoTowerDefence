﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Orc : MonoBehaviour
{
    protected GameObject currentDwarfTarget;
    protected Transform thisTransform;
    protected Animator anim;
    protected float heightAdjustment = 0f;
    // ändra till rprotected
    public GameObject currentTile;
    protected DwarfData myData;
    protected float currentHealth;
    protected bool wasAttacked;
    protected bool isDead;


    public virtual void SetTile(GameObject TILE)
    {
        currentTile = TILE;
        currentTile.GetComponent<Tile>().AddBadGuy(gameObject);
    }

    public void Attacked(float DAMAGE)
    {
        if(DAMAGE <= 0)
            Debug.Log("Orc: Damage can't be zero or less");
        currentHealth -= DAMAGE;
        if (currentHealth < 0)
            isDead = true;
        else
            wasAttacked = true;
    }
    
    protected abstract void ResetValues();
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dwarf : MonoBehaviour
{
    protected Transform thisTransform;
    protected float heightAdjustment = 0f;
    protected GameObject tile;
    protected DwarfData myData;
    protected float currentHealth;
    protected bool wasAttacked;
    protected bool isDead;
    
    

//    protected void ResetHeight()
//    {
//        thisTransform.position = new Vector3(thisTransform.position.x, heightAdjustment, thisTransform.position.z);
//    }

    //protected void ResetRotation()
    public void SetTile(GameObject TILE)
    {
        if (tile != null || TILE.GetComponent<Tile>().HasGoodGuy)
            Debug.Log("Dwarf: Tile is occupied or Dwarf already has a tile");
        else
        {
            tile = TILE;
            tile.GetComponent<Tile>().AddGoodGuy(gameObject);
        }
    }

    public void Attacked(float DAMAGE)
    {
        if(DAMAGE <= 0)
            Debug.Log("Dwarf: Damage can't be zero or less");
        currentHealth -= DAMAGE;
        if (currentHealth < 0)
            isDead = true;
        else
            wasAttacked = true;
    }

    protected abstract void ResetValues();

}

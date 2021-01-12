using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfFighter : Dwarf
{
    [SerializeField] private EnumValueSO axWarriorSO;
    [SerializeField] private int maxHealth;
    [SerializeField] private int strength;

    
    private Animator anim;
    private Tile nextTile;

    private GameObject currentTarget;
    private void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        InvokeRepeating("FighterActions", 1, 4);
        nextTile = TileManagerSystem.TileList[row, column + 1].GetComponent<Tile>();
    }

    private void FighterActions()
    {
        print("in fighter actions");
        if (wasAttacked)
            DwarfAttacked();
        if(isDead == false)
            if (Orc2Attack())
                AttackTarget();
    }

    private void AttackTarget()
    {
        currentTarget.GetComponent<Orc>().TakeDamage(strength);
        anim.SetTrigger("attacking");
    }

    private bool Orc2Attack()
    {
        print("looking for orc");
        if (nextTile.HasBadGuys)
        {
            print("ordOnNextTile");
            currentTarget = nextTile.badGuys[0];
            return true;
        }
        return false;
    }
    

    private void DwarfAttacked()
    {
        if (isDead)
        {
            StartCoroutine(DwarfDeath());
        }
        wasAttacked = false;
    }
    
    
    

    private IEnumerator DwarfDeath()
    {
        anim.SetBool("isDead", true);
        TileManagerSystem.TileList[row, column]
            .GetComponent<Tile>().RemoveGoodGuy();
        TileManagerSystem.TileList[row, column].GetComponent<TileIndicator>().isOccupied = false;
        yield return new WaitForSeconds(4);
        DwarfPool.ReturnDwarf(gameObject, axWarriorSO.name);
    }

    public override void ResetValues() => currentHealth = maxHealth;

}

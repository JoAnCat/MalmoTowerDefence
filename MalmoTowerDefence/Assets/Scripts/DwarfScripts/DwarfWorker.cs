using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfWorker : Dwarf
{
    [SerializeField] private EnumValueSO workerSO; 
    [SerializeField] private int maxHealth;
    public delegate void FullBag(GameObject THIS);
    public static event FullBag OnFullBag;
    private Animator anim;
    private int bag, fullBag;
    [SerializeField] private int diamondsPerSwing = 25;
    private bool isWaitingWithFullBag;
    private bool dieMethodStarted;
    private void Start()
    {
        InvokeRepeating("WorkerActions", 1, 4);
        anim = GetComponent<Animator>();
        fullBag = ScoreManager.StandardCrystalValue;
    }

    private void WorkerActions()
    {
        if (wasAttacked)
            DwarfAttacked();
        else if (fullBag <= bag)
        {
            if(isWaitingWithFullBag == false)
                WaitForBagCollection();
        }
        else
        {
            MineGround();
            print($"mining ground bag {bag.ToString()}");
        }
    }

    private void DwarfAttacked()
    {
        if (isDead && dieMethodStarted == false)
            StartCoroutine(DwarfDeath());
        wasAttacked = false;
    }
    

    private void WaitForBagCollection()
    {
        print("WaitForBagCollection");
        anim.SetBool("hasDiamonds", true);
        isWaitingWithFullBag = true;
        OnFullBag?.Invoke(gameObject);
        // player collect set bag to 0 or -100 and has diamonds to false
    }

    private void MineGround()
    {
        anim.SetTrigger("working");
        bag += diamondsPerSwing;
    }

    private IEnumerator DwarfDeath()
    {
        dieMethodStarted = true;
        anim.SetBool("isDead", true);
        TileManagerSystem.TileList[row, column]
            .GetComponent<Tile>().RemoveGoodGuy();
        TileManagerSystem.TileList[row, column].GetComponent<TileIndicator>().isOccupied = false;
        yield return new WaitForSeconds(4);
        DwarfPool.ReturnDwarf(gameObject, workerSO.name);
    }

    public override void ResetValues() => currentHealth = maxHealth;

    public void CollectBag()
    {
        isWaitingWithFullBag = false;
        bag = 0;
        anim.SetBool("hasDiamonds", false);
    }
}

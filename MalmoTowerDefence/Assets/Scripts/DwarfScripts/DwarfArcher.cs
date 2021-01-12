using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfArcher : Dwarf
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawner;
    [SerializeField] private EnumValueSO archerSO;
    [SerializeField] private int maxHealth;
    [SerializeField] private int strength;
    
    private Animator anim;
    private Transform thisTransform;
    private Vector3 startRotation;
    private float turnSpeed = 1f;
    private GameObject currentTarget;
    private bool hasTarget;
    private int laneLength;
    private void Start()
    {
        thisTransform = transform;
        //thisTransform.rotation = Quaternion.Euler(startRotation);
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        InvokeRepeating("ArcherActions", 1, 4);
        laneLength = TileManagerSystem.TileList.GetLength(1);
    }

//    private void Update()
//    {
//        if (hasTarget && currentTarget != null)
//            thisTransform.rotation =
//                Quaternion.LookRotation(
//                    Vector3.Slerp(thisTransform.rotation.eulerAngles, currentTarget.transform.position, Time.deltaTime * turnSpeed),
//                    Vector3.up);
//        else
//            thisTransform.rotation = Quaternion.LookRotation(
//                Vector3.Slerp(thisTransform.rotation.eulerAngles, startRotation, Time.deltaTime * turnSpeed),
//                Vector3.up);
//    }

    private void ArcherActions()
    {
        if (isDead == false)
        {
            if (Orc2Attack())
                StartCoroutine(AttackTarget());
        }
        else
            StartCoroutine(DwarfDeath());
    }

    private IEnumerator AttackTarget()
    {
        print("trying to fire arrow");
        //send arrow from hand to target with info of target and damage
        //currentTarget.GetComponent<Orc>().TakeDamage(strength);
        anim.SetTrigger("fireingArrow");
        yield return new WaitForSeconds(0.5f);
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawner.position, arrowPrefab.transform.rotation);
        arrow.GetComponent<Arrow>().SetTarget(currentTarget, strength);
    }

    private bool Orc2Attack()
    {
        print("looking for orc");
        for(int i = 1; i < laneLength; i++)
        {
            Tile tileScript = TileManagerSystem.TileList[row, i].GetComponent<Tile>();
            if (tileScript.HasBadGuys)
            {
                hasTarget = true;
                currentTarget = tileScript.badGuys[0];
                print("found orc for archer");
                return true;
            }
        }

        hasTarget = false;
        return false;
    }
    
    private IEnumerator DwarfDeath()
    {
        anim.SetBool("isDead", true);
        TileManagerSystem.TileList[row, column]
            .GetComponent<Tile>().RemoveGoodGuy();
        TileManagerSystem.TileList[row, column].GetComponent<TileIndicator>().isOccupied = false;
        yield return new WaitForSeconds(4);
        DwarfPool.ReturnDwarf(gameObject, archerSO.name);
    }

    public override void ResetValues() => currentHealth = maxHealth;
}

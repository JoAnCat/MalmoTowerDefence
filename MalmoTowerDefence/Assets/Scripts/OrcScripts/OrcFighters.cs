using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class OrcFighters : Orc
{
    private Vector3 startRotation = new Vector3(-5, 175, -28);
    public GameObject nextTile;
    private bool isWalking;
    private bool isAttacking;
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float acceptableDistanceToNextTile = 0.05f;
    private Vector3 nextTilePosition;

    private void Start()
    {
        anim = GetComponent<Animator>();
        thisTransform = transform;
        thisTransform.rotation = Quaternion.Euler(startRotation);
    }

    private void Update()
    {
        if (DwarfToAttack() == false)
            Walk2NextTile();
        else
        {
            if (isWalking)
                StopWalking();
            if(isAttacking == false)
                StartCoroutine(AttackDwarf());
        }
    }

    private IEnumerator AttackDwarf()
    {
        //send damage to dwarf
        anim.SetTrigger("attacking");
        isAttacking = true;
        yield return new WaitForSeconds(4);
        isAttacking = false;

    }

    private void StopWalking()
    {
        anim.SetBool("isWalking", false);
        isWalking = false;
    }

    private void Walk2NextTile()
    {
        print("trying to walk");
        if (isWalking == false)
        {
            anim.SetBool("isWalking", true);
            isWalking = true;
        }
        thisTransform.position = Vector3.Lerp(thisTransform.position, nextTilePosition, Time.deltaTime * moveSpeed);
        if ((thisTransform.position - nextTilePosition).sqrMagnitude < acceptableDistanceToNextTile) 
            SetTile(nextTile);
        
        //lerp
        //if close set next tile
    }

    private bool DwarfToAttack()
    {
        if (TileManagerSystem.TileList[nextTile.GetComponent<Tile>().rowId, nextTile.GetComponent<Tile>().columnId].GetComponent<Tile>().HasGoodGuy)
        {
            currentDwarfTarget = TileManagerSystem.TileList[nextTile.GetComponent<Tile>().rowId, nextTile.GetComponent<Tile>().columnId].GetComponent<Tile>().goodGuy;
            return true;
        }
            
        return false;
    }

    public override void SetTile(GameObject TILE)
    {
        base.SetTile(TILE);
        nextTile = TileManagerSystem.TileList[currentTile.GetComponent<Tile>().rowId, currentTile.GetComponent<Tile>().columnId - 1];
        nextTilePosition = nextTile.transform.position;

    }


    protected override void ResetValues()
    {
        throw new System.NotImplementedException();
    }
}

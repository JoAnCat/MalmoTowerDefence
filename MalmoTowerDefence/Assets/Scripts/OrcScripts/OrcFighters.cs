using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class OrcFighters : Orc
{
    public delegate void LooseGame();
    public static event LooseGame OnLooseGame;
    [SerializeField] private EnumValueSO orcType;
    [SerializeField] private int maxHealth;
    [SerializeField] private int strength;
    private Vector3 startRotation = new Vector3(-5, 175, -28);
    private Vector3 attackPosition;
    public GameObject nextTile;
    private bool isWalking;
    
    private bool isAttacking;
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float acceptableDistanceToNextTile = 0.1f;
    private Vector3 nextTilePosition;
    private float deathWait = 4f;
    private void Start()
    {
        anim = GetComponent<Animator>();
        thisTransform = transform;
        thisTransform.rotation = Quaternion.Euler(startRotation);
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isDead == false)
        {
            if (isAttacking)
                thisTransform.position = attackPosition;
            thisTransform.rotation = Quaternion.Euler(startRotation);
            if (DwarfToAttack() == false)
                Walk2NextTile();
            else
            {
                if (isWalking)
                    StopWalking();
                if (isAttacking == false)
                    StartCoroutine(AttackDwarf());
            }
        }
        else
            StartCoroutine(OrcDeath());

    }

    private IEnumerator OrcDeath()
    {
        anim.SetBool("isDead", true);
        currentTile.GetComponent<Tile>().RemoveBadguy(gameObject);
        currentTile.GetComponent<TileIndicator>().isOccupied = false;
        yield return new WaitForSeconds(deathWait);
        OrcPool.ReturnOrc(gameObject, orcType.name);
        
    }

    private IEnumerator AttackDwarf()
    {
        isAttacking = true;
        attackPosition = thisTransform.position;
        yield return new WaitForSeconds(0.1f);
        anim.SetTrigger("attacking");
        currentDwarfTarget.GetComponent<Dwarf>().TakeDamage(strength);
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
        if (isWalking == false)
        {
            anim.SetBool("isWalking", true);
            isWalking = true;
        }
        thisTransform.position = Vector3.Lerp(thisTransform.position, nextTilePosition, Time.deltaTime * moveSpeed);
        if ((thisTransform.position - nextTilePosition).sqrMagnitude < acceptableDistanceToNextTile) 
            SetTile(nextTile);
        
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
        if(currentTile != null)
            currentTile.GetComponent<Tile>().RemoveBadguy(gameObject);
        base.SetTile(TILE);
        if (currentTile.GetComponent<Tile>().columnId == 1)
            OnLooseGame?.Invoke();
        nextTile = TileManagerSystem.TileList[currentTile.GetComponent<Tile>().rowId, currentTile.GetComponent<Tile>().columnId - 1];
        nextTilePosition = nextTile.transform.position;
    }
    

    public override void ResetValues() => currentHealth = maxHealth;

}

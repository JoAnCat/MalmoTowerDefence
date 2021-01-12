using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject goodGuy;
    public List<GameObject> badGuys;
    private bool hasGoodGuy, hasBadGuys;
    public bool HasGoodGuy => hasGoodGuy;
    public bool HasBadGuys => hasBadGuys;
    public bool hasCrystals;
    public int columnId;
    public int rowId;
    
    private void Start()
    {
        badGuys = new List<GameObject>();
    }

    public void AddGoodGuy(GameObject GOOD_GUY)
    {
        if (goodGuy != null)
            return;
        goodGuy = GOOD_GUY;
        hasGoodGuy = true;
        GetComponent<TileIndicator>().isOccupied = true;
    }

    public void RemoveGoodGuy()
    {
        goodGuy = null;
        hasGoodGuy = false;
    }

    public void AddBadGuy(GameObject BAD_GUY)
    {
        badGuys.Add(BAD_GUY);
        hasBadGuys = true;
    }

    public void RemoveBadguy(GameObject BAD_GUY)
    {
        if (badGuys.Contains(BAD_GUY))
            badGuys.Remove(BAD_GUY);
        if (badGuys.Count < 1)
            hasBadGuys = false;
    }
}

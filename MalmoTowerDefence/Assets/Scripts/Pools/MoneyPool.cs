using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPool : MonoBehaviour
{
    private int maxCrystalsOnBoard = 4;
    [SerializeField] private Transform bankTransform;
    private Vector3 bankPosition;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject crystalPrefab;
    private int standardNumberOfCoin;
    private static int standardCoinValue = 25;
    private Vector3 posAdjustmentCrystal = Vector3.up;
    //private int valueOfCoin;

    private static Queue<GameObject> coinPool = new Queue<GameObject>();
    private static Queue<GameObject> crystalPool = new Queue<GameObject>();

    private void Start()
    {
        bankPosition = bankTransform.position;
        DwarfWorker.OnFullBag += GetCrystal;
        SelectionManager.OnReturnCrystal += ReturnCrystal;
    }

    // flytta till select
    private IEnumerator CoinToBank(Vector3 CRYSTAL_POSITION)
    {
        int coins2Spawn = ScoreManager.StandardCrystalValue / ScoreManager.StandardCoinValue;
        for (int i = 0; i < coins2Spawn; i++)
        {
            GetCoin(CRYSTAL_POSITION);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void GetCoin(Vector3 TILE_POSITION)
    {
        GameObject coin;
        if (coinPool.Count < 1)
        {
            coin = Instantiate(coinPrefab, TILE_POSITION, coinPrefab.transform.rotation);
        }
        else
        {
            coin = coinPool.Dequeue();
            coin.transform.position = TILE_POSITION;
            coin.SetActive(true);
        }

        
    }
    
    

    public static void ReturnCoinFromPool(GameObject COIN)
    {
        if(COIN.tag.Equals("Coin"))
        {
            coinPool.Enqueue(COIN);
            COIN.SetActive(false);
        }
    }
    
    private void GetCrystal(GameObject MASTER)
    {
        Vector3 tilePosition = MASTER.transform.position; 
        GameObject crystal;
        if (crystalPool.Count == 0)
        {
            crystal = Instantiate(crystalPrefab, tilePosition + posAdjustmentCrystal, crystalPrefab.transform.rotation);
        }
        else
        {
            crystal = crystalPool.Dequeue();
            crystal.SetActive(true);
            crystal.transform.position = tilePosition + posAdjustmentCrystal;
        }

        crystal.GetComponent<Crystal>().masterDwarf = MASTER;
    }

    public void ReturnCrystal(GameObject CRYSTAL)
    {
        CRYSTAL.SetActive(false);
        crystalPool.Enqueue(CRYSTAL);
        StartCoroutine(CoinToBank(CRYSTAL.transform.position));
    }
}

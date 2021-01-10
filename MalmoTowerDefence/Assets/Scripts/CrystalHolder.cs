using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrystalHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] crystals;
    [SerializeField] private GameObject crystalPrefab;

    private void Start()
    {
        RotateCrystals();
//        crystals = new GameObject[Random.Range(3, 5)];
//        FillCrystals();

    }

    private void RotateCrystals()
    {
        transform.rotation = Quaternion.Euler(90, 0, Random.Range(0, 360));
    }

    private void FillCrystals()
    {
        for (int i = 0; i < crystals.Length; i++)
        {

            crystals[i] = Instantiate(
                crystalPrefab,
                new Vector3(transform.position.x + Random.Range(-0.75f, 0.75f) , transform.position.y + 0.1f, transform.position.z + Random.Range(-0.75f, 0.75f)), 
                Quaternion.identity, 
                this.transform);
            crystals[i].transform.localScale = new Vector3(1, 1, 1)/10000f;
            crystals[i].transform.rotation = Quaternion.Euler(90, 0, 0);
            
        }
    }
}

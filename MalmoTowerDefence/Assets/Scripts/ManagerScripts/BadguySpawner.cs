using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BadguySpawner : MonoBehaviour
{
    public delegate void LastOrcSent();

    public static event LastOrcSent OnLastOrcSent;
    [SerializeField] private GameObject orcKingPrefab;
    private GameObject tileManager;
    [SerializeField] private List<EnumValueSO> completeOrcArray = new List<EnumValueSO>();
    [SerializeField] private List<float> completeOrcTimer = new List<float>();
    [Range(0, 4)] [SerializeField] private List<int> completeOrcLane = new List<int>();
    private GameObject[] spawnPoints = new GameObject[5];
    private float initialWaitTime = 5;
    
    private void Start()
    {
        StartCoroutine(LateStart());
        StartCoroutine(SpawnOrcs());
    }
    

    private IEnumerator SpawnOrcs()
    {
        yield return new WaitForSeconds(initialWaitTime + 1);
        for(int i = 0; i < completeOrcArray.Count; i++)
        {
            OrcPool.GetOrc(completeOrcArray[i].name, spawnPoints[completeOrcLane[i]]);
            yield return new WaitForSeconds(completeOrcTimer[i]);
        }

        OnLastOrcSent?.Invoke();
    }

    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(initialWaitTime);
        tileManager = GameObject.FindWithTag("TileManager");
        int totalColumns = TileManagerSystem.TileList.GetLength(1);
        spawnPoints[0] = TileManagerSystem.TileList[0, totalColumns - 1];
        spawnPoints[1] = TileManagerSystem.TileList[1, totalColumns - 1];
        spawnPoints[2] = TileManagerSystem.TileList[2, totalColumns - 1];
        spawnPoints[3] = TileManagerSystem.TileList[3, totalColumns - 1];
        spawnPoints[4] = TileManagerSystem.TileList[4, totalColumns - 1];
        
        
    }
}

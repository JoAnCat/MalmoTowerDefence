using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcPool : MonoBehaviour
{
    public delegate void Victory();

    public static event Victory OnVictory ;
    private static Vector3 standardOrcRotation = new Vector3(-10, 10, 35);
    [SerializeField] private GameObject prefabOrcGrunt;
    [SerializeField] private GameObject prefabOrcBerzerker;
    [SerializeField] private GameObject prefabOrcKing;

    [SerializeField] private EnumValueSO orcGruntSO;
    [SerializeField] private EnumValueSO orcBerzerkerSO;
    [SerializeField] private EnumValueSO orcKingSO;
    
    private static Dictionary<string, Queue<GameObject>> orcPool;
    private static Dictionary<string, GameObject> orcPrefabs;
    private bool finalRound;
    private static int orcCounter;
    private void Start()
    {
        orcPool = new Dictionary<string, Queue<GameObject>>()
        {
            {orcGruntSO.name, new Queue<GameObject>()},
            {orcBerzerkerSO.name, new Queue<GameObject>()},
            {orcKingSO.name, new Queue<GameObject>()}
        };
        orcPrefabs = new Dictionary<string, GameObject>()
        {
            {orcGruntSO.name, prefabOrcGrunt},
            {orcBerzerkerSO.name, prefabOrcBerzerker},
            {orcKingSO.name, prefabOrcKing}
        };
        BadguySpawner.OnLastOrcSent += FinalRound;
    }
    
    private void Update()
    {
        if (finalRound && orcCounter == 0)
            OnVictory?.Invoke();
    }

    private void FinalRound() => finalRound = true;

    public static void GetOrc(string TYPE, GameObject TILE)
    {
        if (orcPool.ContainsKey(TYPE))
        {
            GameObject orcSpawn;
            if (orcPool[TYPE].Count == 0)
            {
                print("pool is empty orc");
                orcSpawn = Instantiate(orcPrefabs[TYPE], TILE.transform.position,
                    Quaternion.Euler(standardOrcRotation));
                orcSpawn.transform.GetComponent<OrcFighters>().SetTile(TILE);
            }
            else
            {
                orcSpawn = orcPool[TYPE].Dequeue();
                orcSpawn.SetActive(true);
                orcSpawn.transform.position = TILE.transform.position;
                orcSpawn.transform.rotation = Quaternion.Euler(standardOrcRotation);
                orcSpawn.transform.GetComponent<OrcFighters>().SetTile(TILE);
                orcSpawn.GetComponent<Orc>().ResetValues();
            }
            orcCounter++;
        }
    }

    public static void ReturnOrc(GameObject ORC, string TYPE)
    {
        orcCounter--;
        ORC.SetActive(false);
        orcPool[TYPE].Enqueue(ORC);
    }


}

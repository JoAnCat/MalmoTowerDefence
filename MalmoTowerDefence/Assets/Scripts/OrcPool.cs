using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcPool : MonoBehaviour
{
    private static Vector3 standardDwarfRotation = new Vector3(-10, 10, 35);
    [SerializeField] private GameObject prefabOrcGrunt;
    [SerializeField] private GameObject prefabOrcArcher;
    [SerializeField] private GameObject prefabOrcKing;

    [SerializeField] private EnumValueSO orcGruntSO;
    [SerializeField] private EnumValueSO orcArcherSO;
    [SerializeField] private EnumValueSO orcKingSO;
    
    private static Dictionary<string, Queue<GameObject>> orcPool;
    private static Dictionary<string, GameObject> orcPrefabs;

    private void Start()
    {
        orcPool = new Dictionary<string, Queue<GameObject>>()
        {
            {orcGruntSO.name, new Queue<GameObject>()},
            {orcArcherSO.name, new Queue<GameObject>()},
            {orcKingSO.name, new Queue<GameObject>()}
        };
        orcPrefabs = new Dictionary<string, GameObject>()
        {
            {orcGruntSO.name, prefabOrcGrunt},
            {orcArcherSO.name, prefabOrcArcher},
            {orcKingSO.name, prefabOrcKing}
        };
        
    }

    public static void GetOrc(string TYPE, GameObject TILE)
    {
        if (orcPool.ContainsKey(TYPE))
        {
            print("contains orc");
            if (orcPool[TYPE].Count == 0)
            {
                print("pool is empty orc");
                GameObject orcSpawn = Instantiate(orcPrefabs[TYPE], TILE.transform.position,Quaternion.Euler(standardDwarfRotation));
                orcSpawn.transform.GetComponent<OrcFighters>().SetTile(TILE);
            }
            else
            {
                GameObject orcSpawn;
                orcSpawn = orcPool[TYPE].Dequeue();
                orcSpawn.SetActive(true);
                orcSpawn.transform.position = TILE.transform.position;
                orcSpawn.transform.rotation = Quaternion.Euler(standardDwarfRotation);
                orcSpawn.transform.GetComponent<OrcFighters>().SetTile(TILE);
                //orcSpawn.transform.SetParent(TILE.transform);
            }

        }
        else
            Debug.Log("OrcPool: Pool does not exist!");
    }
}

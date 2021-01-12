using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfPool : MonoBehaviour
{
    private static Vector3 standardDwarfRotation = new Vector3(-10, 10, 35);
    [SerializeField] private GameObject prefabWorker;
    [SerializeField] private GameObject prefabArcher;
    [SerializeField] private GameObject prefabInfantry;
    [SerializeField] private EnumValueSO workerSO; 
    [SerializeField] private EnumValueSO archerSO; 
    [SerializeField] private EnumValueSO axWarriorSO; 

    private static Dictionary<string, Queue<GameObject>> dwarfPool;
    private static Dictionary<string, GameObject> dwarfPrefabs;

    private void Start()
    {
        dwarfPool = new Dictionary<string, Queue<GameObject>>()
        {
            {workerSO.name, new Queue<GameObject>()},
            {archerSO.name, new Queue<GameObject>()},
            {axWarriorSO.name, new Queue<GameObject>()}
        };
        dwarfPrefabs = new Dictionary<string, GameObject>()
        {
            {workerSO.name, prefabWorker},
            {archerSO.name, prefabArcher},
            {axWarriorSO.name, prefabInfantry}
        };
    }

    public static void GetDwarf(string TYPE, GameObject TILE)
    {
        Tile tileScript = TILE.GetComponent<Tile>();
        if (dwarfPool.ContainsKey(TYPE))
        {
            GameObject dwarfSpawn;
            if (dwarfPool[TYPE].Count == 0)
            {
                print("pool is empty dwarf");
                dwarfSpawn = Instantiate(dwarfPrefabs[TYPE], TILE.transform.position,Quaternion.Euler(standardDwarfRotation));
            }
            else
            {
                dwarfSpawn = dwarfPool[TYPE].Dequeue();
                dwarfSpawn.SetActive(true);
                dwarfSpawn.transform.position = TILE.transform.position;
                dwarfSpawn.transform.rotation = Quaternion.Euler(standardDwarfRotation);
                dwarfSpawn.GetComponent<Dwarf>().ResetValues();

            }
            dwarfSpawn.GetComponent<Dwarf>().column = tileScript.columnId;
            dwarfSpawn.GetComponent<Dwarf>().row = tileScript.rowId;
            tileScript.AddGoodGuy(dwarfSpawn);
        }
        
        else
            Debug.Log("DwarfPool: Pool does not exist!");
    }

    public static void ReturnDwarf(GameObject DWARF, string TYPE)
    {
        DWARF.SetActive(false);
        dwarfPool[TYPE].Enqueue(DWARF);
    }
}

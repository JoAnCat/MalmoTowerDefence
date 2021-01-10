using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManagerSystem : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private GameObject portTile;
    [SerializeField] private GameObject noCrystalTile;
    [SerializeField] private GameObject threeCrystalTile;
    [SerializeField] private GameObject fourCrystalTile;
    [SerializeField] private GameObject spawnTile;
    
    private static int columns = 11;
    private static int rows = 5;
    private static GameObject[,] tileList = new GameObject[5, 11];
    public static GameObject[,] TileList => tileList;

    private static float rowDistance = 1.5f;
    [SerializeField] private static float columnDistance = 1.3f;
    private float spawnVisualBuffer = 2f;

    //public GameObject[] lanes = new GameObject[5];
    [SerializeField] private TileType[] laneOne = new TileType[columns];
    [SerializeField] private TileType[] laneTwo = new TileType[columns];
    [SerializeField] private TileType[] laneThree = new TileType[columns];
    [SerializeField] private TileType[] laneFour = new TileType[columns];
    [SerializeField] private TileType[] laneFive = new TileType[columns];

    private void Start()
    {
        Transform thisTransform = transform;
        Vector3 spawnPosition = startPosition;
        SpawnTiles(spawnPosition, thisTransform, 0, laneOne);
        spawnPosition += Vector3.right * rowDistance;
        SpawnTiles(spawnPosition, thisTransform, 1, laneTwo);
        spawnPosition += Vector3.right * rowDistance;
        SpawnTiles(spawnPosition, thisTransform, 2, laneThree);
        spawnPosition += Vector3.right * rowDistance;
        SpawnTiles(spawnPosition, thisTransform, 3, laneFour);
        spawnPosition += Vector3.right * rowDistance;
        SpawnTiles(spawnPosition, thisTransform, 4, laneFive);
    }

    private void SpawnTiles(Vector3 SPAWN_POSITION, Transform THIS_TRANSFORM, int CURRENT_ROW, TileType[] CURRENT_LANE)
    {
        for (int i = 0; i < columns; i++)
        {
            GameObject tile;
            switch (CURRENT_LANE[i])
            {
                case TileType.PortSide:
                    tile = portTile;
                    break;
                case TileType.NoCrystals:
                    tile = noCrystalTile;
                    break;
                case TileType.ThreeCrystals:
                    tile = threeCrystalTile;
                    break;
                case TileType.FourCrystals:
                    tile = fourCrystalTile;
                    break;
                case TileType.Spawn:
                    tile = spawnTile;
                    break;
                default:
                    tile = noCrystalTile;
                    break;
            }

            tileList[CURRENT_ROW, i] = Instantiate(tile, SPAWN_POSITION, Quaternion.Euler(90, 0, 0), THIS_TRANSFORM);
            if(i == columns - 2)
                SPAWN_POSITION += new Vector3(0, 0, columnDistance * spawnVisualBuffer);
            else
                SPAWN_POSITION += new Vector3(0, 0, columnDistance);
            tileList[CURRENT_ROW, i].GetComponent<Tile>().rowId = CURRENT_ROW;
            tileList[CURRENT_ROW, i].GetComponent<Tile>().columnId = i;
        }
    }
}

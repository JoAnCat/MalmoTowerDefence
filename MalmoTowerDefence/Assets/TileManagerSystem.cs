using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManagerSystem : MonoBehaviour
{
    [SerializeField] private GameObject Tile;

    [SerializeField] private static float rowDistance = 1.5f;
    [SerializeField] private static float columnDistance = 1.5f;

    public GameObject[] lanes = new GameObject[5];
    
    
}

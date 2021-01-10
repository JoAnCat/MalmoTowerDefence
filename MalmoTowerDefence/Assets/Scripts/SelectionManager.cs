using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> sloths;
    //public delegate bool CanSelectDwarf(string NAME);
    public delegate void UnitPurchase(string NAME);

    //public static event CanSelectDwarf OnCanSelectDwarf;
    public static event UnitPurchase OnUnitBought;
    private TileIndicator selectedTileScript;
    private GameObject selectedTile;
    private Vector3 selectedTilePos;
    private Transform selectedObject;
    private Vector3 selectedObjectOrigin;
    //private Camera myCam;
    private Camera unitCam, mainCam;
    private bool isSelected, hasUnitSelected, isDropable;


    public bool HasUnitSelected => hasUnitSelected;


    private void Start()
    {
        mainCam = Camera.main;
        unitCam = GameObject.FindWithTag("SecondCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (isDropable)
        {
            isDropable = false;
            selectedTileScript.thisTileSelected = false;
        }
        if (Input.GetMouseButtonDown(0))
            MouseLeftBegin();
        if (hasUnitSelected)
        {

            SearchForTile();
            if (isDropable)
            {
                selectedObject.position = selectedTilePos + new Vector3(0, 0, 40);
            }
            else
            {
                Vector3 temp = mainCam.ScreenToWorldPoint(Input.mousePosition);
                selectedObject.position = new Vector3(temp.x * 2f - 5, 0, temp.z + 40);
            }

        }
        if (Input.GetMouseButtonUp(0))
            MouseLeftEnd();
    }

    private void SearchForTile()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag.Equals("Tile"))
            {
                print("bingo!");
                selectedTile = hit.transform.gameObject;
                selectedTileScript = hit.transform.GetComponent<TileIndicator>();
                selectedTilePos = hit.transform.position;
                
                if (selectedTileScript.isOccupied == false)
                {
                    selectedTileScript.thisTileSelected = true;
                    isDropable = true;
                }
            }
        }
    }

    private void MouseLeftBegin()
    {
        Ray unitRay = unitCam.ScreenPointToRay(Input.mousePosition);
        Ray mainRay = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(unitRay, out hit))
        {
            if (hit.transform.tag.Equals("Unit"))
            {
                print("object selected");
                if(CheckTargetIsOpen4Business(hit.transform.gameObject.GetComponent<DwarfSelectPrefabs>().unitType.name))
                selectedObject = hit.transform;
                selectedObjectOrigin = selectedObject.position;
                hasUnitSelected = true;
            }
        }
    }

    private bool CheckTargetIsOpen4Business(string NAME)
    {
        foreach (GameObject sloth in sloths)
        {
            PurschaseBlockedIndicator pbi = sloth.transform.GetComponent<PurschaseBlockedIndicator>();
            if (pbi.UnitName.Equals(NAME) && pbi.openForBusiness)
                return true;
        }

        return false;
    }

    private void MouseLeftEnd()
    {
        if (hasUnitSelected)
        {
            if (isDropable)
            {
                print("trying to create new dwarf from pool");
                DwarfPool.GetDwarf(selectedObject.transform.GetComponent<DwarfSelectPrefabs>().unitType.name, selectedTile);
                OnUnitBought?.Invoke(selectedObject.transform.GetComponent<DwarfSelectPrefabs>().unitType.name);
            }
            selectedObject.position = new Vector3(selectedObjectOrigin.x, selectedObjectOrigin.y, selectedObjectOrigin.z);
        }


        //Resetting 
        selectedObject = null;
        isDropable = false;
        hasUnitSelected = false;
    }
}

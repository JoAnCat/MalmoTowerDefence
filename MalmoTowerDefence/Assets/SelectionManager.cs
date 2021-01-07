using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private TileIndicator selectedTileScript;
    private Vector3 selectedTilePos;
    private Transform selectedObject;
    private Vector3 selectedObjectOrigin;
    //private Camera myCam;
    private Camera unitCam, mainCam;
    private bool isSelected, hasUnitSelected, isDropable;
    

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
            selectedTileScript.hasUnitSelected = false;
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
                selectedTileScript = hit.transform.GetComponent<TileIndicator>();
                selectedTilePos = hit.transform.position;
                if (selectedTileScript.isOccupied == false)
                {
                    selectedTileScript.hasUnitSelected = true;
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
                selectedObject = hit.transform;
                selectedObjectOrigin = selectedObject.position;
                hasUnitSelected = true;
            }
        }
    }
    
    private void MouseLeftEnd()
    {
        if (hasUnitSelected)
        {
            if (isDropable)
            {
            
            }
            else
            {
                print($"selectedObject.position {selectedObject.position.ToString()}");
                selectedObject.position = new Vector3(selectedObjectOrigin.x, selectedObjectOrigin.y, selectedObjectOrigin.z);
                print($"selectedObject.position {selectedObject.position.ToString()}");
                print("trying to return");
            }
        }


        //Resetting 
        selectedObject = null;
        isDropable = false;
        hasUnitSelected = false;
    }
}

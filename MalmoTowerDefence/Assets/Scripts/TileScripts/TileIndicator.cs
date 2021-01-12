using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileIndicator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color colorNormal;
    [SerializeField] private Color colorSelected;
    private Camera mainCam;
    public bool isOccupied, thisTileSelected;
    //[SerializeField] private Transform selectionManager;
    private SelectionManager selectionManagerScript;
    private Tile tileScript;
    private void Start()
    {
        mainCam = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectionManagerScript = GameObject.FindWithTag("SelectionManager").GetComponent<SelectionManager>();
        tileScript = transform.GetComponent<Tile>();
    }

    private void Update()
    {
        if (isOccupied == false)
        {
            if (selectionManagerScript.HasUnitSelected && thisTileSelected)
            {
                if(selectionManagerScript.SelectedUnitIsWorker && tileScript.hasCrystals == false)
                    spriteRenderer.color = colorNormal;
                else
                    spriteRenderer.color = colorSelected;
            }
            else
            {
                spriteRenderer.color = colorNormal;
            }
        }
        else
            spriteRenderer.color = colorNormal;
    }
}

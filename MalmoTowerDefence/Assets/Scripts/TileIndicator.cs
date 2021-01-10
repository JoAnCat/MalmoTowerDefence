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
    
    private void Start()
    {
        mainCam = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectionManagerScript = GameObject.FindWithTag("SelectionManager").GetComponent<SelectionManager>();
    }

    private void Update()
    {
        if (isOccupied == false)
        {
            if (selectionManagerScript.HasUnitSelected && thisTileSelected)
            {
                spriteRenderer.color = colorSelected;
            }
            else
            {
                spriteRenderer.color = colorNormal;
            }
        }
    }
}

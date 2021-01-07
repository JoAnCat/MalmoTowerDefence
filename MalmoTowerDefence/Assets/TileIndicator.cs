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
    public bool hasUnitSelected, isOccupied;
    
    private void Start()
    {
        mainCam = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isOccupied == false)
        {
            if (hasUnitSelected)
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

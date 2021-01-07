using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyNDrag : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private Transform myTransform;
    //private Camera myCam;
    private Camera unitCam, mainCam;
    private bool isSelected = false;

    private void Start()
    {
        myTransform = transform;
        mainCam = Camera.main;
        unitCam = GameObject.FindWithTag("SecondCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            testMouseDown();
        if (isSelected)
        {
            Vector3 temp = mainCam.ScreenToWorldPoint(Input.mousePosition);
            myTransform.position = new Vector3(temp.x * 2f - 5, 0, temp.z + 40);
        }

        if (Input.GetMouseButton(0) == false)
            isSelected = false;
    }

    private void testMouseDown()
    {
        Ray ray = unitCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (transform == hit.transform)
            {
                isSelected = true;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 temp = mainCam.ScreenToWorldPoint(Input.mousePosition);
        myTransform.position = new Vector3(temp.x, temp.y, 0);
        print("TRying to move Cube");
        //myTransform.position = myTransform.position + new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUnit : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject objectToSpawn;
    
    public void OnPointerDown(PointerEventData EVENT_DATA)
    {
        //Instantiate(objectToSpawn);
    }
}

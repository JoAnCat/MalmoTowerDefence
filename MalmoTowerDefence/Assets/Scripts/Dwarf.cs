using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dwarf : MonoBehaviour
{
    protected Transform thisTransform;
    protected float heightAdjustment = 0f;

    protected void ResetHeight()
    {
        thisTransform.position = new Vector3(thisTransform.position.x, heightAdjustment, thisTransform.position.z);
    }
    
    //protected void ResetRotation()


}

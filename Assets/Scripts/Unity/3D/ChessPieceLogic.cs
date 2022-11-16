using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    
    // Alert on mouse click on this object
    void OnMouseDown()
    {
        Debug.Log("Clicked on " + gameObject.name + DateTime.Now.ToFileTime());
    }
    
    
}

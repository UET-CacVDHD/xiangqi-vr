using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        Debug.Log(transform.position);
        Debug.Log(new Vector3(0, 1, 1 ) + new Vector3(1, 1, 0));
    }
}

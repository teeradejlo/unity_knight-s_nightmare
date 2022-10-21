using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMove : MonoBehaviour {
    
    // Use this for initialization
    void Start () {

	}

    public void HeadPositionChange ()
    {
        transform.Translate(new Vector3(20, 0, 0));
    } 
}

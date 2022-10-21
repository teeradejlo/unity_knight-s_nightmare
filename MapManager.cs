using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    [HideInInspector]
    public int roomCount;

	// Use this for initialization
	void Start () {
        roomCount = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.childCount > 3)
        {
            Destroy(GetComponent<Transform>().GetChild(0).gameObject);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletingEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("SelfDestroy", 0.5f);
	}

    void SelfDestroy ()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpMeteorFall : MonoBehaviour {

    Vector3 meteorVelocity;

    float timer = 0;
    bool timerReached = false;

    // Use this for initialization
    void Start () {

        meteorVelocity.y = -9;
	}
	
	// Update is called once per frame
	void Update () {

        if (!timerReached)
        {
            timer += Time.deltaTime;
        }

        if (!timerReached && timer > 0.5f)
        {
            timerReached = true;
        }

        if (timerReached)
            transform.Translate(meteorVelocity * Time.deltaTime);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeKnight : MonoBehaviour {

    Vector3 knightVelocity;

    Camera mainCamera;

    float timer = 0;
    bool timerReached = false;

    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {

        if (!timerReached)
        {
            timer += Time.deltaTime;
            knightVelocity.x = 8;
        }

        if (!timerReached && timer > 1f)
        {
            timerReached = true;
        }

        if (timerReached)
            knightVelocity.x = 30;

        transform.Translate(knightVelocity * Time.deltaTime);

        if (transform.position.x > mainCamera.transform.position.x + 25)
        {
            Destroy(gameObject);
        }
    }
}

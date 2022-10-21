using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogLance : MonoBehaviour {

    Vector3 lanceSpeed;

    Camera mainCamera;

    int lanceSpeedX;

    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;
        
        lanceSpeedX = Random.Range(9, 12);

        lanceSpeed.x = lanceSpeedX;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > 7)
        {
            transform.Translate(lanceSpeed * Time.deltaTime);
        }

        if (transform.position.x < mainCamera.transform.position.x - 30)
        {
            Destroy(gameObject);
        }
    }
}

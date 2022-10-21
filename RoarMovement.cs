using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoarMovement : MonoBehaviour {

    Vector3 roarVelocity;

    int roarSpeedX;

    Camera mainCamera;

    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;

        roarSpeedX = Random.Range(17, 22);

        roarVelocity.x = roarSpeedX;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(roarVelocity * Time.deltaTime);

        if (transform.position.x > mainCamera.transform.position.x + 25)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSeed : MonoBehaviour {

    float movementAngle;

    Vector3 bulletVelocity;

	// Use this for initialization
	void Start () {

        movementAngle = transform.parent.GetComponent<PumpkinBoss>().bulletAngle;

        transform.parent = null;

	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y > 15)
        {
            Destroy(gameObject);
        }

        bulletVelocity.x = Mathf.Cos(movementAngle * Mathf.Deg2Rad);
        bulletVelocity.y = Mathf.Sin(movementAngle * Mathf.Deg2Rad);

        transform.Translate(bulletVelocity * 15.6f * Time.deltaTime);
    }
}

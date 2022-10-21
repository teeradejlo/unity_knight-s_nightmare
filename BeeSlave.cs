using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSlave : MonoBehaviour {

    GameObject player;

    Vector3 beeSlaveVelocity;

    float distancePlayerBossX;
    float distancePlayerBossY;
    float displacementPLayeBeeSlave;

    float beeSlaveSpeed;

    float timer = 0;
    bool lifeSpanGone = false;

    float randomSpeed;

    Animator beeAnim;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Character");

        beeAnim = GetComponent<Animator>();
        beeAnim.SetBool("Die", false);

        randomSpeed = Random.Range(9f, 11f);
    }
	
	// Update is called once per frame
	void Update () {
        distancePlayerBossX = player.transform.position.x - transform.position.x;
        distancePlayerBossY = player.transform.position.y - transform.position.y;
        displacementPLayeBeeSlave = Mathf.Sqrt(Mathf.Pow(distancePlayerBossX, 2) + Mathf.Pow(distancePlayerBossY, 2));

        beeSlaveVelocity.x = distancePlayerBossX / displacementPLayeBeeSlave;
        beeSlaveVelocity.y = distancePlayerBossY / displacementPLayeBeeSlave;

        if(displacementPLayeBeeSlave > 13)
        {
            beeSlaveSpeed = 15f;
        }
        else if (displacementPLayeBeeSlave < 10)
        {
            beeSlaveSpeed = randomSpeed;
        }

        if (lifeSpanGone)
        {
            beeSlaveVelocity.x = 0;
            beeSlaveVelocity.y = 1;
            beeSlaveSpeed = -9;
        }

        transform.Translate(beeSlaveVelocity * beeSlaveSpeed * Time.deltaTime);

        if (!lifeSpanGone)
        {
            timer += Time.deltaTime;
        }

        if (!lifeSpanGone && timer > 5)
        {
            beeAnim.SetBool("Die", true);
            lifeSpanGone = true;
            Vector2 Scaler = transform.localScale;
            Scaler.y *= -1;
            transform.localScale = Scaler;
            gameObject.tag = "DieAtk";
        }
    }
}

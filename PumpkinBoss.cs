using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinBoss : MonoBehaviour {
    
    Vector3[] fallPosition;
    
    public GameObject pumpMeteor;
    public GameObject seed;
    GameObject mapManager;
    GameObject player;
    Camera mainCamera;

    int fallPositionGet;

    Vector3 bossSpeed;
    float timer = 0;
    bool canAttack = false;

    [HideInInspector]
    public float bulletAngle;

    bool meteorCreated = false;
    bool bulletCreated = false;

    float distancePlayerBossX;
    float distancePlayerBossY;

    // Use this for initialization
    void Start () {

        mapManager = GameObject.Find("Map Manager");
        player = GameObject.Find("Character");

        fallPosition = new Vector3[2];

        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        distancePlayerBossX = player.transform.position.x - transform.position.x;
        distancePlayerBossY = player.transform.position.y - transform.position.y;

        if (distancePlayerBossX > 23)
        {
            bossSpeed.x = 15.6f;
            bossSpeed.y = 0;
        }
        else if (distancePlayerBossX < 11)
        {
            bossSpeed.x = 5.3f;
            bossSpeed.y = 0;
        }
        else if (distancePlayerBossX < 17)
        {
            bossSpeed.x = 7.8f;
            bossSpeed.y = 0;
        }

        if (player.transform.position.x < transform.position.x)
        {
            if (!player.GetComponent<Player>().died)
            {
                bossSpeed.x = distancePlayerBossX * 5 / 6;
                bossSpeed.y = distancePlayerBossY * 5 / 6;
            }
        }
        
        transform.Translate(bossSpeed * Time.deltaTime);

        if (mapManager.transform.GetComponent<MapManager>().roomCount > 13)
        {
            if (!canAttack)
            {
                timer += Time.deltaTime;
            }

            if (!canAttack && timer > 1.5)
            {
                canAttack = true;
                Invoke("ResetAtk", 5f);
            }

            if (canAttack)
            {
                if (!meteorCreated)
                {
                    if (!bulletCreated)
                    {
                        BulletStorm();
                    }
                }

            }
        }
        else if (mapManager.transform.GetComponent<MapManager>().roomCount > 4)
        {
            if (!canAttack)
            {
                timer += Time.deltaTime;
            }
        
            if (!canAttack && timer > 2)
            {
                canAttack = true;
                Invoke("ResetAtk", 2f);
            }
        
            if (canAttack)
            {
                if (!meteorCreated)
                    PumpkinMeteor();
            }
        }
    }

    void PumpkinMeteor ()
    {
        meteorCreated = true;

        fallPosition[0] = new Vector3(mainCamera.transform.position.x + 8f, mainCamera.transform.position.y + 9f, -5);

        fallPosition[1] = new Vector3(mainCamera.transform.position.x + 14f, mainCamera.transform.position.y + 9f, -5);

        fallPositionGet = Random.Range(0, fallPosition.Length);

        Instantiate(pumpMeteor, fallPosition[fallPositionGet], Quaternion.identity);
        
    }

    void BulletStorm ()
    {
        if (canAttack)
        {
            bulletCreated = true;

            bulletAngle = Random.Range(-10f, 17f);

            GameObject bullet = Instantiate(seed, new Vector3(transform.position.x, transform.position.y - 3f, -6), Quaternion.Euler(0, 0, bulletAngle), gameObject.transform);

            bullet.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            Invoke("BulletStorm", 0.4f);
        }
    }

    void ResetAtk ()
    {
        timer = 0;
        canAttack = false;
        meteorCreated = false;
        bulletCreated = false;
    }
    
}

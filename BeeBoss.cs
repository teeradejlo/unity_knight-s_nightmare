using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBoss : MonoBehaviour {

    public GameObject beeSlave;
    public GameObject beeKnight;
    GameObject mapManager;
    GameObject player;
    Camera mainCamera;

    Vector3 bossSpeed;
    float timer = 0;
    bool canAttack = false;

    float distancePlayerBossX;
    float distancePlayerBossY;

    bool beeSlaveCreated = false;
    bool beeKnightCreated = false;
    Vector3 beeSpawnPosition;

    int knightSpawnPositionGet;
    Vector3[] knightSpawnPosition;

    // Use this for initialization
    void Start () {
        mapManager = GameObject.Find("Map Manager");
        player = GameObject.Find("Character");
        mainCamera = Camera.main;

        knightSpawnPosition = new Vector3[3];
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
        else if (distancePlayerBossX < 9)
        {
            bossSpeed.x = 5.3f;
            bossSpeed.y = 0;
        }
        else if (distancePlayerBossX < 15)
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

            if (!canAttack && timer > 2)
            {
                canAttack = true;
                Invoke("ResetAtk", 3f);
            }

            if (canAttack)
            {
                if (!beeSlaveCreated)
                {
                    if (!beeKnightCreated)
                    {
                        CallBeeKnight();
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

            if (!canAttack && timer > 1)
            {
                canAttack = true;
                Invoke("ResetAtk", 0.5f);
            }

            if (canAttack)
            {
                if (!beeSlaveCreated)
                    CallBeeSlave ();
            }
        }
    }

    void CallBeeSlave ()
    {
        beeSlaveCreated = true;

        beeSpawnPosition = new Vector3(transform.position.x + 3f, transform.position.y - 4f, -7);

        Instantiate(beeSlave, beeSpawnPosition, Quaternion.identity);
    }

    void CallBeeKnight()
    {
        beeKnightCreated = true;

        knightSpawnPosition[0] = new Vector3(mainCamera.transform.position.x - 15f, mainCamera.transform.position.y, -7);
        knightSpawnPosition[1] = new Vector3(mainCamera.transform.position.x - 15f, mainCamera.transform.position.y + 4f, -7);
        knightSpawnPosition[2] = new Vector3(mainCamera.transform.position.x - 15f, mainCamera.transform.position.y - 4f, -7);

        knightSpawnPositionGet = Random.Range(0, knightSpawnPosition.Length);

        Instantiate(beeKnight, knightSpawnPosition[knightSpawnPositionGet], Quaternion.identity);
    }

    void ResetAtk ()
    {
        timer = 0;
        canAttack = false;
        beeSlaveCreated = false;
        beeKnightCreated = false;
    }
}

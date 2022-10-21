using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBoss : MonoBehaviour {

    public GameObject lance;
    public GameObject smallRoar;
    GameObject mapManager;
    GameObject player;
    Camera mainCamera;

    Vector3 bossSpeed;
    float timer = 0;
    bool canAttack = false;

    float distancePlayerBossX;
    float distancePlayerBossY;

    bool dogRoarCreated;

    bool dogLanceCreated;
    int dogLancePositionGet;
    int dogLanceScale;
    float dogLanceAngle;

    float roarDelayTime;

    Vector3[] lanceSpawnPosition;

    // Use this for initialization
    void Start () {
        mapManager = GameObject.Find("Map Manager");
        player = GameObject.Find("Character");
        mainCamera = Camera.main;
        roarDelayTime = 2.5f;

        lanceSpawnPosition = new Vector3[3];
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

        if (mapManager.transform.GetComponent<MapManager>().roomCount > 20)
            roarDelayTime = 1f;

        if (mapManager.transform.GetComponent<MapManager>().roomCount > 13)
        {
            if (!canAttack)
            {
                timer += Time.deltaTime;
            }

            if (!canAttack && timer > roarDelayTime)
            {
                canAttack = true;
                Invoke("ResetAtk", 1f);
            }

            if (canAttack)
            {
                if (!dogLanceCreated)
                {
                    if (!dogRoarCreated)
                    {
                        CreateSmallRoar();
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
                Invoke("ResetAtk", 3);
            }

            if (canAttack)
            {
                if (!dogLanceCreated)
                {
                    CreateDogLance();
                }
            }
        }
    }

    void CreateDogLance()
    {
        dogLanceCreated = true;

        lanceSpawnPosition[0] = new Vector3(mainCamera.transform.position.x + 8f, mainCamera.transform.position.y + 17f, 0);
        lanceSpawnPosition[1] = new Vector3(mainCamera.transform.position.x + 14f, mainCamera.transform.position.y + 17f, 0);
        lanceSpawnPosition[2] = new Vector3(mainCamera.transform.position.x + 20f, mainCamera.transform.position.y + 17f, 0);

        dogLancePositionGet = Random.Range(0, lanceSpawnPosition.Length);
        dogLanceScale = Random.Range(15, 21);
        dogLanceAngle = Random.Range(240f, 300f);

        GameObject spawnedLance = Instantiate(lance, lanceSpawnPosition[dogLancePositionGet], Quaternion.identity);
        
        spawnedLance.transform.localScale = new Vector3(dogLanceScale, dogLanceScale, 1);
        spawnedLance.transform.Rotate(new Vector3(0, 0, dogLanceAngle));

    }

    void CreateSmallRoar()
    {
        dogRoarCreated = true;
        Instantiate(smallRoar, new Vector3(transform.position.x + 4f, transform.position.y, 0), Quaternion.identity);
    }

    void ResetAtk()
    {
        timer = 0;
        canAttack = false;
        dogLanceCreated = false;
        dogRoarCreated = false;
    }
}

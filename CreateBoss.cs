using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoss : MonoBehaviour {

    public AudioSource bGM;

    public float rayLength;

    public LayerMask playerMask;

    GameObject bossManager;

    public GameObject[] bossType;

    int bossSpawned;

    bool bossCreated;

    Camera mainCamera;

    // Use this for initialization
    void Start () {
        bossCreated = false;

        mainCamera = Camera.main;

		bossManager = GameObject.Find("Boss Manager");

        bossSpawned = PlayerPrefs.GetInt("Boss Type");
    }
	
	// Update is called once per frame
	void Update () {

        RaycastHit2D playerCheck = Physics2D.Raycast(transform.position, Vector2.down, rayLength, playerMask);
        Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.red);

        if (playerCheck)
        {
            if (!bossCreated)
            {
                bossCreated = true;
                bGM.Play();
                Debug.Log("Create Boss");
                Instantiate(bossType[bossSpawned], new Vector3(mainCamera.transform.position.x - 25f, mainCamera.transform.position.y, 0), Quaternion.identity, bossManager.transform);
            }

            rayLength = 0;
        }
    }
}

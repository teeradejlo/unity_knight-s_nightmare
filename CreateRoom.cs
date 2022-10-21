using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoom : MonoBehaviour {

    public float rayLength;

    public LayerMask playerMask;

    public GameObject[] roomPattern;

    public GameObject mapManager;

    GameObject headMap;

    bool roomCreated;

	// Use this for initialization
	void Start () {
        mapManager = GameObject.Find("Map Manager");
        headMap = GameObject.Find("Head");

        roomCreated = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D playerCheck = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + 9f, transform.position.z), Vector2.down, rayLength, playerMask);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 9f, transform.position.z), Vector2.down * rayLength, Color.red);

        if (playerCheck)
        {
            if (!roomCreated)
            {
                if (mapManager.transform.GetComponent<MapManager>().roomCount < 24)
                {
                    roomCreated = true;
                    mapManager.transform.GetComponent<MapManager>().roomCount += 1;
                    headMap.transform.GetComponent<HeadMove>().HeadPositionChange();
                    Debug.Log("Create Room");
                    Debug.Log(mapManager.transform.GetComponent<MapManager>().roomCount);
                    Instantiate(roomPattern[Random.Range(0, roomPattern.Length - 1)], new Vector3(transform.position.x + 30.4f, 0, 0), Quaternion.identity, mapManager.transform);
                }
                else
                {
                    roomCreated = true;
                    mapManager.transform.GetComponent<MapManager>().roomCount += 1;
                    headMap.transform.GetComponent<HeadMove>().HeadPositionChange();
                    Debug.Log("Create Door Room");
                    Instantiate(roomPattern[3], new Vector3(transform.position.x + 30.4f, 0, 0), Quaternion.identity, mapManager.transform);
                }

                rayLength = 0;
            }
        }
    }
}

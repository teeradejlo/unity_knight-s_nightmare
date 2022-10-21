using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityChange : MonoBehaviour {

    public GameObject spike;

    int abilityGet;

    float timer = 0;
    bool activated = true;

    int deactivatedTime;

    Vector3[] spikePosition;

    Animator platformAnim;

	// Use this for initialization
	void Start () {

        platformAnim = GetComponent<Animator>();

        spikePosition = new Vector3[2];
        timer = 0;
        activated = true;

        deactivatedTime = Random.Range(3, 7);

        platformAnim.SetBool("Blinking", false);

        gameObject.SetActive(true);

        abilityGet = Random.Range(1, 4);
        if (abilityGet == 1)
        {
            //Spike
            spikePosition[0] = new Vector3(transform.position.x + 1.8f, transform.position.y + 0.22f, 0);
            spikePosition[1] = new Vector3(transform.position.x - 0.4f, transform.position.y + 0.22f, 0);

            GameObject newSpike = Instantiate(spike, spikePosition[Random.Range(0, spikePosition.Length)], Quaternion.identity, gameObject.transform);

            newSpike.transform.localScale = new Vector3(0.07f, 0.07f, 0);
        }
        else if (abilityGet == 3)
        {
            gameObject.tag = "Through";
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (abilityGet == 2)
        {
            //Active/Unactive
            if (activated)
            {
                timer += Time.deltaTime;
            }

            if (activated && timer > deactivatedTime / 2)
            {
                platformAnim.SetBool("Blinking", true);
            }

            if (activated && timer > deactivatedTime)
            {
                activated = false;
                gameObject.SetActive(false);
            }
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour {

    Player player;

    float timer = 0;
    [HideInInspector]
    public bool timerReached = false;

    void Start () {
        player = GetComponent<Player>();	
	}
	
	void Update () {

        if (!timerReached)
        {
            timer += Time.deltaTime;
        }

        if (!timerReached && timer > 1)
        {
            timerReached = true;
        }

        if (timerReached)
        {
            Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (!Pausing.gameIsPaused)
                player.SetDirectionalInput(directionalInput);

            //Handle all Jumping
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                player.OnjumpInputDown();
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                player.OnjumpInputUp();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                player.Dashing();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                player.Dashing();
            }
        }
    }
}

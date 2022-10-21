using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerCollider : MonoBehaviour {

    GameObject bGM;

    GameObject canva;
    public GameObject transition;

    bool gameEnd;

    GameObject lifeManager;

    public GameObject playerDieEffect;
    public GameObject bossAtkDeletionEffect;

	// Use this for initialization
	void Start () {
        lifeManager = GameObject.Find("Life");

        canva = GameObject.Find("Canvas");
        bGM = GameObject.Find("BG music");

        gameEnd = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameEnd)
        {
            if (collision.tag == "Player")
            {
                if (gameObject.tag == "Boss" || gameObject.tag == "Lava")
                {
                    Instantiate(playerDieEffect, collision.transform.position, Quaternion.identity);
                    collision.GetComponent<Player>().velocity.y = 0;
                    lifeManager.GetComponent<LifeManager>().AllLifeDeletion();
                    collision.GetComponent<Player>().died = true;
                }
                else if (gameObject.tag == "Door")
                {
                    if (PlayerPrefs.GetInt("Boss Type") == 0)
                        PlayerPrefs.SetInt("Pumpkin Boss", 1);
                    else if (PlayerPrefs.GetInt("Boss Type") == 1)
                        PlayerPrefs.SetInt("Bee Boss", 1);
                    else if (PlayerPrefs.GetInt("Boss Type") == 2)
                        PlayerPrefs.SetInt("Dog Boss", 1);

                    PlayerPrefs.Save();

                    collision.GetComponent<Player>().velocity.x = 0;
                    collision.GetComponent<Player>().velocity.y = 0;

                    gameEnd = true;
                    StartCoroutine(GameEnd("StageCleared"));
                    return;
                }
                else if (gameObject.tag != "DieAtk")
                {
                    if (collision.GetComponent<Player>().life > 0)
                    {
                        if (!collision.GetComponent<Player>().immune)
                        {
                            collision.GetComponent<Player>().life -= 1;
                            collision.GetComponent<Player>().immune = true;
                            lifeManager.GetComponent<LifeManager>().LifeDeletion();
                        }

                        if (collision.GetComponent<Player>().life == 0)
                        {
                            Instantiate(playerDieEffect, collision.transform.position, Quaternion.identity);
                            lifeManager.GetComponent<LifeManager>().LifeDeletion();
                            collision.GetComponent<Player>().died = true;
                        }

                        Instantiate(bossAtkDeletionEffect, transform.position, Quaternion.identity);
                        Destroy(gameObject);
                    }
                }
            }
        }

        if (gameObject.tag == "Lava")
        {
            if (collision.tag == "BossAtk" || collision.tag == "DieAtk")
            {
                Instantiate(bossAtkDeletionEffect, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                return;
            }
        }
    }

    IEnumerator GameEnd(string sceneToGo)
    {
        bGM.GetComponent<Animator>().SetTrigger("FadeOut");
        Instantiate(transition, canva.transform.position, Quaternion.identity, canva.transform);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneToGo);
    }
}

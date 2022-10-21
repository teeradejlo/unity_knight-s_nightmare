using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    public GameObject transition;

    public string sceneToGo;

    public int bossType;

    public void MoveScene ()
    {
        StartCoroutine(Moving());
    }

    IEnumerator Moving ()
    {
        Instantiate(transition, transform.parent.position, Quaternion.identity, transform.parent.parent);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneToGo);
    }

    public void CloseGame ()
    {
        StartCoroutine(Closing());
    }

    IEnumerator Closing ()
    {
        Instantiate(transition, transform.parent.position, Quaternion.identity, transform.parent.parent);
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }

    public void ReGame ()
    {
        PlayerPrefs.SetInt("Pumpkin Boss", 0);
        PlayerPrefs.SetInt("Bee Boss", 0);
        PlayerPrefs.SetInt("Dog Boss", 0);

        PlayerPrefs.SetInt("SaveData", 1);

        PlayerPrefs.Save();
    }

    public void SetBossType ()
    {
        PlayerPrefs.SetInt("Boss Type", bossType);
    }
}

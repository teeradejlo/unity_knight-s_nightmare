using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecKSaveData : MonoBehaviour {

    void Awake()
    {
        PlayerPrefs.GetInt("SaveData", 0);
    }

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("SaveData") == 0)
            gameObject.SetActive(false);
        else if (PlayerPrefs.GetInt("SaveData") == 1)
            gameObject.SetActive(true);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBadgeUpdate : MonoBehaviour {

    public string bossName;

    public Sprite clearBadge;
    public Sprite normalBadge;

    Button button;

    void Awake()
    {
        button = GetComponent<Button>();

        if (PlayerPrefs.GetInt(bossName) == 0)
            button.GetComponent<Image>().sprite = normalBadge;
        else if (PlayerPrefs.GetInt(bossName) == 1)
            button.GetComponent<Image>().sprite = clearBadge;
    }
    
}

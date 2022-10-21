using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBadgeUpdate : MonoBehaviour {

    public Sprite clearBadge;
    public Sprite normalBadge;

    public string[] bossName;

    SpriteRenderer badgeRender;

    void Awake()
    {
        badgeRender = GetComponent<SpriteRenderer>();

        if (PlayerPrefs.GetInt(bossName[0]) == 1 && PlayerPrefs.GetInt(bossName[1]) == 1 && PlayerPrefs.GetInt(bossName[2]) == 1)
            badgeRender.sprite = clearBadge;
        else
            badgeRender.sprite = normalBadge;
    }
}

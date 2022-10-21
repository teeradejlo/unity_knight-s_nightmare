using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour {

    public Sprite[] clearedBadgeType;

    SpriteRenderer badgeRender;

    void Awake()
    {
        badgeRender = GetComponent<SpriteRenderer>();

        badgeRender.sprite = clearedBadgeType[PlayerPrefs.GetInt("Boss Type")];
    }
}

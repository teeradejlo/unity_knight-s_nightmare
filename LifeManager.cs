using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour {
    
    public void LifeDeletion ()
    {
        int amountChild = transform.childCount;

        if (amountChild > 0)
            Destroy(transform.GetChild(amountChild - 1).gameObject);
    }

    public void AllLifeDeletion()
    {
        int amountChild = transform.childCount;

        for (int i = 0; i < amountChild; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

}

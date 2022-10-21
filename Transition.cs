using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {

    public int inOut;

    Animator transAnim;

	// Use this for initialization
	void Start () {
        transAnim = GetComponent<Animator>();

        if (inOut == 1)
            In();
        else
            Out();
	}
	
	void In ()
    {
        transAnim.SetTrigger("Start");
        Invoke("DestroySelf", 1f);
    }
    
    void Out ()
    {
        transAnim.SetTrigger("End");
        Invoke("DestroySelf", 2f);
    }

    void DestroySelf ()
    {
        Destroy(gameObject);
    }
}

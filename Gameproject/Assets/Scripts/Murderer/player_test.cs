using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_test : MonoBehaviour {
    Murderer murderer;


    void Start () {
        GameObject ob = GameObject.FindGameObjectWithTag("Murderer");
        if (ob == null)
            Debug.Log("Player don't get murderer object.");
        else
            murderer = ob.GetComponent<Murderer>();

    }
	
	void Update () {
        if (murderer == null) return;

        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("Get Mouse Button");
            murderer.UpdateSpot(gameObject.transform.position);
        }

    }
}

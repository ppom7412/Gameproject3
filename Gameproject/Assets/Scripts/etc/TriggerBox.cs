using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBox : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("<"+gameObject.name+ ">  OnTriggerEnter()");
        LevelManager.Instance.ChangeLevel(0);
    }
}

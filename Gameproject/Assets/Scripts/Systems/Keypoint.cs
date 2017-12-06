using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypoint : MonoBehaviour {
    public int key;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GettingKeyFromObject keys = other.gameObject.GetComponent<GettingKeyFromObject>();

            if (keys == null) {
                ErrorAdmin.ErrorMessegeFromObject("Player don't have GettingKeyFromObject Script.", "OnTriggerEnter()", gameObject);
                return;
            }

            keys.AddKeyPoint(key);
        }
    }
}

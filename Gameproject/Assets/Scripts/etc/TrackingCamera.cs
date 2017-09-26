using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour {
    public GameObject trackingObject;
    public Vector3 keepPos;
    public Vector3 keepRot;

	void Update () {
        if (trackingObject){
            transform.position = Vector3.Lerp(transform.position, trackingObject.transform.position + ( keepPos), Time.deltaTime);
            //transform.rotation = trackingObject.transform.rotation;
            //transform.eulerAngles = transform.eulerAngles + keepRot;
        }
	}
}

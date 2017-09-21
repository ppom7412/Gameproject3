using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveSphare : MonoBehaviour {

	private static SphereCollider col;
	[SerializeField]
	private float MaxRadius = 10;
	// Use this for initialization
	void Start () {
		col = gameObject.GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		if (col.radius <= MaxRadius) {
			col.radius += Time.deltaTime * 1.50f;
		} else {
			Destroy (gameObject);
		}
	}
}

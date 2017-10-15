using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveManager : MonoBehaviour {

	[SerializeField]
	private Material material;

	private float[] isActive;
	private float[] isEnemy;
	private Vector4[] circlePoint;

	private int arrayLength;


	void Awake () {
		material.SetFloatArray("activeArray", new float[10]);
		material.SetFloatArray("enemyArray", new float[10]);
		//material.SetVectorArray ("pointArray", new Vector4[10]);
	}

	// Use this for initialization
	void Start () {
		isActive = new float[10];
		isEnemy = new float[10]; 
		circlePoint = new Vector4[10];

		arrayLength = 10;

		for(int i = 0; i < arrayLength; i+=2) {
			isActive[i] = 1.0f;
			isEnemy[i] = 0.0f;
			circlePoint [i] = new Vector4 (0, 0, i, 0);
		}
		for(int i = 1; i < arrayLength; i+=2) {
			isActive[i] = 1.0f;
			isEnemy[i] = 1.0f;
			circlePoint [i] = new Vector4 (1, 0, i, 0);
		}


	}
	
	// Update is called once per frame
	void Update () {
		material.SetFloatArray("activeArray", isActive);
		material.SetFloatArray("enemyArray", isEnemy);
		material.SetVectorArray("pointArray", circlePoint);
	}
}

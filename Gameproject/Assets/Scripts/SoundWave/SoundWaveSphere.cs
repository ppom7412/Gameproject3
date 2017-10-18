using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveSphere : MonoBehaviour {

	private  SphereCollider col;
	[SerializeField]
	private float MaxRadius = 10;

    private SoundWaveData soundWaveData;

    void Start () {
		col = gameObject.GetComponent<SphereCollider>();

        soundWaveData.isActive = 1.0f;
        soundWaveData.isEnemy = 0.0f;
        soundWaveData.radius = 1.0f;
        soundWaveData.circlePoint = new Vector4(0, 0, 0);
    }

	void Update () {

        Debug.Log("업데이트 실행");

        if (col.radius <= MaxRadius) {
            Debug.Log("반지름 변경");

            col.radius += Time.deltaTime * 1.50f;
		} else {
			col.radius = 0.5f;
		}
	}

    public SoundWaveData GetSoundWaveData()
    {
        return soundWaveData;
    }
}

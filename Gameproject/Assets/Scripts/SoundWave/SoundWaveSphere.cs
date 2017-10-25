using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveSphere : MonoBehaviour {

	private  SphereCollider col;
	[SerializeField]
	private float MaxRadius = 10;

    // 이 오브젝트의 데이터를 저장하는 구조체
    private SoundWaveData soundWaveData;
    // 외부에서 받은 데이터를 저장하는 구조체
    private SoundWaveData takeData;

    void Start () {
		col = gameObject.GetComponent<SphereCollider>();

        soundWaveData.isActive = 0.0f;
        soundWaveData.isEnemy = 0.0f;
        soundWaveData.radius = 0.5f;
        soundWaveData.circlePoint = new Vector4(0, 30,-30);

        ClineTakeData();
    }

	void Update () {

        SetSoundWaveData(takeData);

        if (soundWaveData.isActive == 1.0f)
        {
            transform.position = soundWaveData.circlePoint;
            if (col.radius <= MaxRadius) {
                col.radius += Time.deltaTime * 5.0f;
                takeData.radius = col.radius;
               
            } else {
                ClineTakeData();
                col.radius = takeData.radius;
            }
        }

	}
    // 외부에서 이 오브젝트의 접근시 사용
    public SoundWaveData GetSoundWaveData()
    {
        return soundWaveData;
    }

    // 외부에서 이 오브젝트의 데이터를 변경하기 위해 사용
    public void SetTakeData(SoundWaveData externalData)
    {
        takeData.isActive = externalData.isActive;
        takeData.isEnemy = externalData.isEnemy;
        takeData.radius = externalData.radius;
        takeData.circlePoint = externalData.circlePoint;
    }

    // 데이터 갱신
    private void SetSoundWaveData(SoundWaveData SetData)
    {
        soundWaveData.isActive = SetData.isActive;
        soundWaveData.isEnemy = SetData.isEnemy;
        soundWaveData.radius = SetData.radius;
        soundWaveData.circlePoint = SetData.circlePoint;
    }
    // 외부 데이터 초기화
    private void ClineTakeData()
    {
        takeData.isActive = 0.0f;
        takeData.isEnemy = 0.0f;
        takeData.radius = 0.5f;
        takeData.circlePoint = new Vector4(0, 30, -30);
    }
}

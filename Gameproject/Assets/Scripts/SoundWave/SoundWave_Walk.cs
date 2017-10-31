using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave_Walk : MonoBehaviour {

    private bool isGround;
    private SoundWaveData data;
    
    [SerializeField]
    private GameObject SoundWaveManager;
    private GameObject SoundWave;

    void Start () {
        isGround = false;
        data.isActive = 1.0f;
        data.isEnemy = 0.0f;
        data.radius = 0.5f;
        data.subRadius = 0.6f;
        data.circlePoint = transform.position;
    }

    private void OnTriggerEnter(Collider col)
    {
        // WalkCheckArea에 들어갔을시 초기화
        if (col.tag == "WalkCheckArea")
        {
            Debug.Log("WalkCheckArea");
            if (isGround)
            {
                isGround = false;
            }
        }

        
    }

    private void OnTriggerStay(Collider col)
    {
        // 바닥에 발이 닿았을시 처리
        if (col.tag == "Floor")
        {
            Debug.Log("Floor");
            if (!isGround)
            {
                isGround = true;

                data.circlePoint = transform.position;
                SoundWave = SoundWaveManager.GetComponent<SoundWaveManager>().SearchInactiveObject();
                SoundWave.GetComponent<SoundWaveSphere>().SetTakeData(data);

            }

        }
    }



}

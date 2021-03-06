﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave_Enemy : MonoBehaviour {
    private bool isActive;
    private bool isGround;
    private float timer;
    [SerializeField]
    private float waitingTime = 0.3f;
    private SoundWaveData data;

    [SerializeField]
    private GameObject SoundWaveManager;
    private GameObject SoundWave;

    [SerializeField]
    private GameObject WalkCheckArea;

    void Start()
    {
        timer = 0.0f;
        isActive = false;
        isGround = false;
        data.isActive = 1.0f;
        data.isEnemy = 1.0f;
        data.radius = 0.5f;
        data.subRadius = 0.6f;
        data.circlePoint = transform.position;
    }

    private void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                timer = 0;
                isActive = false;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!isActive)
        {
            // WalkCheckArea에 들어갔을시 초기화
            if (col.tag == "WalkCheckArea")
            {
                if (isGround)
                {
                    isGround = false;
                    isActive = true;
                    Debug.Log("WalkCheckArea");
                }
            }
        }

        // 바닥에 발이 닿았을시 처리
        if (col.tag == "Floor")
        {

            if (!isGround)
            {
                Debug.Log("Floor");
                isGround = true;

                data.circlePoint = WalkCheckArea.transform.position;
                SoundWave = SoundWaveManager.GetComponent<SoundWaveManager>().SearchInactiveObject();
                SoundWave.GetComponent<SoundWaveSphere>().SetTakeData(data);

            }

        }

    }
}

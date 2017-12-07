using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour {

    private SoundWaveData data;
    private GameObject SoundWaveManager;
    private GameObject SoundWave;

    // Use this for initialization
    void Start ()
    { 
        data.isActive = 1.0f;
        data.isEnemy = 0.0f;
        data.radius = 0.5f;
        data.subRadius = 0.6f;
        data.circlePoint = transform.position;
        SoundWaveManager = GameObject.Find("SoundWaveManager");
    }

    public void FlagChangePlayer()
    {
        data.isEnemy = 0.0f;
    }
    public void FlagChangeMurderer()
    {
        data.isEnemy = 1.0f;
    }


    public void ActiveSoundWave()
    {
        SoundWave = SoundWaveManager.GetComponent<SoundWaveManager>().SearchInactiveObject();
        SoundWave.GetComponent<SoundWaveSphere>().SetTakeData(data);
    }
}

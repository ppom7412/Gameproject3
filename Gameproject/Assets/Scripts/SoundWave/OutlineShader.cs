using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineShader : MonoBehaviour
{
    [SerializeField]
    private Material Outline;
    [SerializeField]
    private GameObject MyGameObject;
    private Material MyMaterial;

    private bool isActive;

    private float timer;
    [SerializeField]
    private float waitingTime = 2.0f;

    private SoundWaveData soundWaveData;

    void Start()
    {
        timer = 0.0f;
        isActive = false;

        MyGameObject = gameObject;
        MyMaterial = MyGameObject.GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                timer = 0;
                MyGameObject.GetComponent<MeshRenderer>().material = MyMaterial;
                isActive = false;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!isActive)
        {
            if (col.tag == "SoundWave")
            {
                soundWaveData = col.GetComponent<SoundWaveSphere>().GetSoundWaveData();
                if (soundWaveData.isEnemy == 0.0f)
                {
                    MyGameObject.GetComponent<MeshRenderer>().material = Outline;
                    isActive = true;
                }
            }
        }
    }
}

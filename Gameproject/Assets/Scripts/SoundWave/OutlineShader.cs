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

    void Start()
    {
        MyMaterial = MyGameObject.GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "SoundWave")
        {
            MyGameObject.GetComponent<MeshRenderer>().material = Outline;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "SoundWave")
        {
            MyGameObject.GetComponent<MeshRenderer>().material = Outline;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "SoundWave")
        {
            MyGameObject.GetComponent<MeshRenderer>().material = MyMaterial;
        }
    }
}

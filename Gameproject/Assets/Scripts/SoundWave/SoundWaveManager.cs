using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SoundWaveData
{
    public float isActive;
    public float isEnemy;
    public float radius;
    public Vector4 circlePoint;
}


public class SoundWaveManager : MonoBehaviour {

	[SerializeField]
	private Material material;

    GameObject[] soundWaveSpheres;

    private float[] isActiveArray;
	private float[] isEnemyArray;
    private float[] radiusArray;
	private Vector4[] circlePointArray;

    private SoundWaveData tempData;

    private int arrayLength;


	void Awake () {
		material.SetFloatArray("activeArray", new float[10]);
		material.SetFloatArray("enemyArray", new float[10]);
        material.SetFloatArray("radiusArray", new float[10]);
        material.SetVectorArray ("pointArray", new Vector4[10]);
	}


	void Start () {
        isActiveArray = new float[10];
        isEnemyArray = new float[10];
        radiusArray = new float[10];
        circlePointArray = new Vector4[10];

        soundWaveSpheres = new GameObject[10];

        arrayLength = 10;

        for (int i = 0; i < arrayLength; i++)
        {
            int nameNumber;
            nameNumber = i + 1;
            soundWaveSpheres[i] = GetChildObj("Sphere" + nameNumber.ToString());
        }

        for (int i = 0; i < arrayLength; i++)
        {
            tempData = soundWaveSpheres[i].GetComponent<SoundWaveSphere>().GetSoundWaveData();

            isActiveArray[i] = tempData.isActive;
            isEnemyArray[i] = tempData.isEnemy;
            radiusArray[i] = tempData.radius;
            circlePointArray[i] = tempData.circlePoint;
        }

  //      for (int i = 0; i < arrayLength; i+=2) {
  //          isActiveArray[i] = 1.0f;
  //          isEnemyArray[i] = 0.0f;
  //          radiusArray[i] = 1.0f;
  //          circlePointArray[i] = new Vector4 (0, 0, i, 0);
		//}
		//for(int i = 1; i < arrayLength; i+=2) {
  //          isActiveArray[i] = 1.0f;
  //          isEnemyArray[i] = 1.0f;
  //          radiusArray[i] = 1.0f;
  //          circlePointArray[i] = new Vector4 (1, 0, i, 0);
		//}


	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < arrayLength; i++)
        {
            if (isActiveArray[i] == 1.0f)
            {

                radiusArray[i] += Time.deltaTime * 1.50f;
            }            
        }


        material.SetFloatArray("activeArray", isActiveArray);
		material.SetFloatArray("enemyArray", isEnemyArray);
        material.SetFloatArray("radiusArray", radiusArray);
        material.SetVectorArray("pointArray", circlePointArray);
	}

    GameObject GetChildObj(string strName)
    {
        Transform[] AllData = gameObject.GetComponentsInChildren<Transform>();
        Debug.Log(strName);

        foreach (Transform Obj in AllData)
        {
            if (Obj.name == strName)
            {
                Debug.Log("오브젝트를 찾았습니다");
                return Obj.gameObject;
            }
        }
        Debug.Log("오브젝트를 찾지 못하였습니다");
        return null;
    }
}

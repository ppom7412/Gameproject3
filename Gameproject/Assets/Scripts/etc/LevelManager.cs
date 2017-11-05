using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    static private LevelManager instance;
    static public LevelManager Instance {
        get { return instance; }
        private set {
            if (!instance) {
                instance = (LevelManager) GameObject.FindObjectOfType(typeof(LevelManager));
                if (!instance){
                    GameObject instanceObject = new GameObject();
                    instanceObject.name = "LevelManager";
                    instance = (LevelManager) instanceObject.AddComponent(typeof(LevelManager));
                }
            }

        }
    }

    private GameObject loadingMap;
    private GameObject presentMap;
    private GameObject player;
    private bool isLoad;
    private bool isTimer;
    private float fTime;
    private int nextlevelnum;

    // -. Map 데이터
    //public enum LevelName {invalid=-1, tutorial, size }
    public LevelData[] leveldatas;

    void Awake()
    {
        if (!instance)
        {
            instance = (LevelManager)GameObject.FindObjectOfType(typeof(LevelManager));
            if (!instance)
            {
                GameObject instanceObject = new GameObject();
                instanceObject.name = "LevelManager";
                instance = (LevelManager)instanceObject.AddComponent(typeof(LevelManager));
            }
        }
    }

    void Start () {
        // 1. 로딩맵 생성
        loadingMap = GameObject.FindGameObjectWithTag("LoadingMap");
        if (!loadingMap) {
            Debug.Log("로딩 맵 생성");
            loadingMap = new GameObject();
            presentMap.name = "LoadingMap";
            loadingMap.tag = "LoadingMap";
            loadingMap.transform.position = Vector3.zero;
        }

        // 2. 현재맵 생성
        presentMap = GameObject.FindGameObjectWithTag("PresentMap");
        if (!presentMap) {
            Debug.Log("현재 맵 생성");
            presentMap = new GameObject();
            presentMap.name = "Map";
            presentMap.tag = "PresentMap";
            presentMap.transform.position = Vector3.zero;
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update () {
        // -. 로딩 현황이나 시간을 측정
        if (isTimer)
            fTime += Time.deltaTime;

        if (fTime > 10.0f && !isLoad)
        {
            player.transform.position = leveldatas[nextlevelnum].point;
            fTime = 0.0f;
            isTimer = false;
        }

    }

    void CreatePresentMap(int _mapNum)
    {
        if (leveldatas.Length < _mapNum) return;

        //데이터를 기반한 맵 생성.
        for (int i=0; i< leveldatas[_mapNum].objects.Length; ++i)
        {
            //생성 진행도
            Instantiate(leveldatas[_mapNum].objects[i], presentMap.transform);
        }
        isLoad = false;
    }

    void ClearThePresentMap()
    {
        foreach (Transform child in presentMap.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void ChangeLevel(int _levelnum)
    {
        if (_levelnum > leveldatas.Length) return;
        nextlevelnum = _levelnum;
        isLoad = true;
        isTimer = true;
        player.transform.position = loadingMap.transform.position;

        ClearThePresentMap();
        CreatePresentMap(_levelnum);
    }
}

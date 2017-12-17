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

    private GameObject player;
    private GameObject murderer;
    private bool isLoad;
    private bool isTimer;
    private float fTime;
    private int nextlevelnum;

    // -. Map 데이터
    //public enum LevelName {invalid=-1,Loading, tutorial, size }
    public LevelData[] leveldatas;
    public float waitingTime;
    public int currlevel = 0;   //0은 로딩맵

    void Awake()
    {
       
    }

    void Start () {
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
        currlevel = 0;
        isLoad = false;
        player = GameObject.FindGameObjectWithTag("Player");
        murderer = GameObject.FindGameObjectWithTag("Murderer");
    }
	
	void Update () {
        if (isTimer)
            fTime += Time.deltaTime;

        if (fTime > waitingTime && isLoad)
        {
            player.transform.position = leveldatas[nextlevelnum].playerPoint;
            MoveToLevelMap();

            fTime = 0.0f;
            isTimer = false;
            isLoad = false;
            currlevel = nextlevelnum;
            nextlevelnum = 0;
        }
    }

    void MoveToLoadingMap()
    {
        player.transform.position = leveldatas[0].playerPoint;
        murderer.transform.position = leveldatas[0].murdererPoint;

        Murderer murdererState = murderer.GetComponent<Murderer>();
        murdererState.CurrStateSetEmpty();

        murdererState.ResetSoundSpot();
        murdererState.ResetWaypoint();
    }

    void CurrentLevelSetUnactive()
    {
        leveldatas[currlevel].objectParent.SetActive(false);

        foreach (GameObject obj in leveldatas[currlevel].objectParent.GetComponentsInChildren<GameObject>())
        {
            obj.SetActive(false);
        }
    }

    void NextLevelSetActive()
    {
        leveldatas[nextlevelnum].objectParent.SetActive(true);

        foreach (GameObject obj in leveldatas[nextlevelnum].objectParent.GetComponentsInChildren<GameObject>())
        {
            obj.SetActive(true);
        }
    }

    void MoveToLevelMap()
    {
        player.transform.position = leveldatas[nextlevelnum].playerPoint;
        murderer.transform.position = leveldatas[nextlevelnum].murdererPoint;

        Murderer murdererState = murderer.GetComponent<Murderer>();
        murdererState.CurrStateSetWait();
    }

    public void ChangeLevel(int _nextlevel)
    {
        if (_nextlevel < 0 || _nextlevel > leveldatas.Length)
        {
            ErrorAdmin.ErrorMessege("_nextlevel is unexit.", "ChangeLevel(int _nextlevel)");
            return;
        }

        nextlevelnum = _nextlevel;

        MoveToLoadingMap();

        CurrentLevelSetUnactive();
        NextLevelSetActive();

        isLoad = true;
    }
}

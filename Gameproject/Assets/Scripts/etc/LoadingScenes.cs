using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScenes : MonoBehaviour
{
    public Text loadingText;
    public Text timeText;
    public float limitTime;

    private float fTime = 0;
    public bool isStart = false;
    private AsyncOperation async_operation;

    void Update()
    {
        if (!isStart)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                LoadScene("SceneA");
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                LoadScene("SceneB");
            }
        }
        else
        {
            fTime += Time.deltaTime;
            timeText.text = "<" + fTime.ToString("F2") + ">";
            loadingText.text = (async_operation.progress * 100.0f).ToString("F1");

            if (fTime >= limitTime && async_operation.progress >= 0.9f)
                async_operation.allowSceneActivation = true;
        }
    }

    public void LoadScene(string _nextName)
    {
        isStart = true;
        loadingText.text = _nextName + "을 로딩합니다...";

        StopAllCoroutines();
        StartCoroutine(LoadYourAsyncScene(_nextName));
    }

    IEnumerator LoadYourAsyncScene(string _sceneName)
    {
        async_operation = Application.LoadLevelAsync(_sceneName);
        async_operation.allowSceneActivation = false; //제한시간을 걸기위함

        yield return async_operation;
    }
}

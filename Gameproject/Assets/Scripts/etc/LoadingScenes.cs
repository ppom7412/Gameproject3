using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScenes : MonoBehaviour {
    public Text loadingText;

    private float fTime;
    private bool IsDone = false;
    private AsyncOperation async_operation;

	void Start () {
    }
	
	void Update () {
        fTime += Time.deltaTime;
        loadingText.text = fTime.ToString();

        if (fTime >= 10)
            async_operation.allowSceneActivation = true;
	}

    public void LoadScene(string _nextName)
    {
        StopAllCoroutines();
        StartCoroutine(StartLoad(_nextName));
    }

    public IEnumerator StartLoad(string _sceneName)
    {
        async_operation = Application.LoadLevelAsync(_sceneName);
        async_operation.allowSceneActivation = false;

        if (IsDone == false) {
            IsDone = true;

            while (async_operation.progress < 0.9f) {
                loadingText.text = async_operation.progress.ToString();

                yield return true;
            }
        }
    }
}

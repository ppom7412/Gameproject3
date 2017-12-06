using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    //public AnimationClip pauseClip;
    //public AnimationClip unpauseClip;

    //public GameObject pausePanel;
    public GameObject optionPanel;
    public GameObject menuPanel;

    public bool isPause = false;
    public bool isMenu = false;
    public bool isOption = false;
    public Animator animator;

	void Start () {
        animator = GetComponent<Animator>();

        if(!animator)
         ErrorAdmin.ErrorMessege("animator not Found Animator.", "Start()");

        animator.updateMode = AnimatorUpdateMode.UnscaledTime;

        optionPanel.SetActive(false);
        menuPanel.SetActive(false);
    }

	void Update () {

        animator.SetBool("isPause", isPause);

        if (Input.GetKeyDown(KeyCode.Escape)){
            if (!isPause)
                Pause();
        }
    }

    void ChangeStateMunePanel(bool _value)
    {
        isMenu = _value;
        menuPanel.active = isMenu;
    }

    void ChangeStateOptionPanel(bool _value)
    {
        isOption = _value;
        optionPanel.active = isOption;
    }

    public void UnPause()
    {
        isPause = false;
        ChangeStateMunePanel(false);
        ChangeStateOptionPanel(false);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        isPause = true;
        ChangeStateMunePanel(true);
        ChangeStateOptionPanel(false);
        Time.timeScale = 0;
    }

    public void OpenMenu()
    {
        ChangeStateMunePanel(true);
        ChangeStateOptionPanel(false);
    }

    public void OpenOption()
    {
        ChangeStateMunePanel(false);
        ChangeStateOptionPanel(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

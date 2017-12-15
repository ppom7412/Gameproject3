using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatWindow : MonoBehaviour {
    private static ChatWindow instance;
    private bool textTimerSwitch;
    private float countdown;

    public Text showTextBoxObject;
    public float defaultTime;

    public string[] texts;

    void Awake()
    {
        if (!instance)
        {
            //필드에 해당하는 인스턴스를 가진 오브젝트을 찾는다.
            instance = (ChatWindow)FindObjectOfType(typeof(ChatWindow));

            if (!instance)
            {
                GameObject chatObject = new GameObject();
                chatObject.name = "ChatController";
                instance = chatObject.AddComponent(typeof(ChatWindow)) as ChatWindow;
            }
        }

        gameObject.name = "ChatController";
    }

    void Start () {
        ResetChatWindow();
    }
	
	void Update () {

        if (textTimerSwitch){
            if (countdown <= 0)
                ResetChatWindow();

            countdown -= Time.deltaTime;
        }
	}

    void SetUpChatWindow(float _limitTime, string _chat)
    {
        if (!showTextBoxObject){
            ErrorAdmin.ErrorMessege("ChatController don't get showTextBoxObject. And showTextBoxObject type is Text in UI Object", "SetUpChatWindow()");
            return;
        }

        showTextBoxObject.text = _chat;
        countdown = _limitTime;
        textTimerSwitch = true;

        if (_limitTime < 0)
            countdown = defaultTime;
    }

    void ResetChatWindow()
    {
        if (!showTextBoxObject){
            ErrorAdmin.ErrorMessege("ChatController don't get showTextBoxObject. And showTextBoxObject type is Text in UI Object", "ResetChatWindow()");
            return;
        }

        showTextBoxObject.text = "";
        countdown = 0;
        textTimerSwitch = false;
    }

    public static void StartTextInChatWondow( string _chatContain, float _limitTime = -1)
    {
        instance.SetUpChatWindow(_limitTime, _chatContain);
    }

    public static void StartTextInChatWondow(int _chatContain, float _limitTime = -1)
    {
        if (_chatContain > instance.texts.Length) return;

        instance.SetUpChatWindow(_limitTime, instance.texts[_chatContain]);
    }
}

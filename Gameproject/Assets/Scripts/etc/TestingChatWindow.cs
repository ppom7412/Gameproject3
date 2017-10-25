using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingChatWindow : MonoBehaviour {

	void Update () {
        if (Input.GetMouseButtonDown(1))
            ChatWindow.StartTextInChatWondow("소리가 난다");

        if (Input.GetMouseButtonDown(0))
            ChatWindow.StartTextInChatWondow("할 수 없어");

        if (Input.GetMouseButtonDown(2))
            ChatWindow.StartTextInChatWondow("하고 싶지 않아");
    }
}

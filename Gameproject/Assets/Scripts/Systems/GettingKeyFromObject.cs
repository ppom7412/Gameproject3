using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingKeyFromObject : MonoBehaviour {
    public enum GettingType { MasterKey, GettingKey }
    public GettingType thisType;

    [Header("KeySetting")]
    public KeyCode keycode;
    [Range(0.1f, 100.0f)]
    public float distance;

    [SerializeField]
    private int[] getKeyPoints;
    private const int maxKeyCount = 20;

    private int doorLayerMask;


    void Start () {
        getKeyPoints = new int[maxKeyCount];
        doorLayerMask = 1 << LayerMask.NameToLayer("Door");

        if (thisType == GettingType.MasterKey)
            SetMasterKey();

        else if (thisType == GettingType.GettingKey)
            ResetGettingKey();
    }

    void Update()
    {
        TryToOpenTheDoor();

    }

    void SetMasterKey()
    {
        for (int i = 0; i < maxKeyCount; ++i)
            getKeyPoints[i] = i;
    }

    void ResetGettingKey()
    {
        for (int i = 0; i < maxKeyCount; ++i)
            getKeyPoints[i] = -1;
    }

    int SearchKeypoint(int _target)
    {
        for (int i = 0; i < maxKeyCount; i++) {
            if (getKeyPoints[i] < 0)
                continue;

            if (getKeyPoints[i] == _target)
                return i;
        }

        return -1;
    }

    void TryToOpenTheDoor()
    {
        if (Input.GetKeyDown(keycode))
        {
            Debug.Log("Push! Key");
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, distance, doorLayerMask))
            //if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, distance, doorLayerMask))
            {
                Debug.Log("TryToOpenTheDoor");
                DoorScript door = hitInfo.collider.gameObject.GetComponentInParent<DoorScript>();
                if (door == null) hitInfo.collider.gameObject.GetComponent<DoorScript>();
                if (door == null)
                {
                    ErrorAdmin.ErrorMessegeFromObject("Object is not Door, But Object have Layer is Door", "TryToOpenTheDoor()", hitInfo.collider.gameObject);
                    return;
                }
                Debug.Log("This is Door!");
                UseTheKeys(door);
                ActiveDoor(door);
            }
        }
    }

    public void AddKeyPoint(int _point)
    {
        for (int i = 0; i < maxKeyCount; i++)
        {
            if (getKeyPoints[i] < 0 && getKeyPoints[i] != _point){
                getKeyPoints[i] = _point;
                break;
            }
        }
    }

    public void ActiveDoor(DoorScript _door)
    {
        if (thisType == GettingType.MasterKey)
        {
            //부모의 위치를 통해 한번 지나간 문은 저장했다 다시 바꿔주는 코드가 필요함.
            _door.ChangeDoorStateWithMasterKey();
        }
        else if (thisType == GettingType.GettingKey)
        {
            _door.ChangeDoorStateWithAnyKey();
        }

        Debug.Log(" === == === === == =");
    }

    public void UseTheKeys(DoorScript _door){
        Debug.Log("UseTheKeys");

        int keyValue = SearchKeypoint(_door.doorNum);

        //만약 마스터 키일 경우엔 언락하지 않고 문을 연다.
        if (thisType == GettingType.MasterKey){
            return;
        }

        //키를 가지고 있지 않아 실패
        else if (keyValue == -1) {
            return;
        }

        //키를 가지고 있음. 언락
        _door.DoorUnlock();
        getKeyPoints[keyValue] = -1;

        ChatWindow.StartTextInChatWondow(0);
    }
}

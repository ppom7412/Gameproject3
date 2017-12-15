using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour {
   //rigidbody 꼭 확인
    public DoorScript mydoor;

    void OpenTheDoor()
    {
        if (mydoor == null) return;
        if (mydoor.open) return;

        mydoor.ChangeDoorStateWithMasterKey();
    }

    void CloseTheDoor()
    {
        if (mydoor == null) return;
        if (!mydoor.open) return;

        mydoor.ChangeDoorStateWithMasterKey();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("틱");
        DoorScript door = other.GetComponent<DoorScript>();
        if (door == null) return;

        if (mydoor != null)
            CloseTheDoor();

        mydoor = door;
        OpenTheDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("톡");
        DoorScript door = other.GetComponent<DoorScript>();
        if (door == null) return;

        CloseTheDoor();
        mydoor = null;
    }
}

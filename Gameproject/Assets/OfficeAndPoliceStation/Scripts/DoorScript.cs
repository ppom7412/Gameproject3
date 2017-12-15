using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorScript : MonoBehaviour {

	public bool open = false;
    public bool unlock = true;
    public int doorNum;
	public float doorOpenAngle = 90f;
	public float doorCloseAngle = 0f;
	public float smooth = 2f;
 

    void Start () 
	{
        //layer에 Door추가 해야함
        gameObject.layer = LayerMask.NameToLayer("Door");
        for (int i=0; i< transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Door");
        }
    }

    public void DoorUnlock()
    {
        unlock = true;
    }

    public void ChangeDoorStateWithAnyKey()
	{
        if (!unlock) return;
        

        open = !open;
        GetComponent<DoorSound>().FlagChangePlayer();
        GetComponent<DoorSound>().ActiveSoundWave();
        GetComponent<AudioSource>().Play();
        Debug.Log("문을 연다.");
	}

    //부모의 위치를 통해 한번 지나간 문은 저장했다 다시 바꿔주는 코드가 필요함.
    public void ChangeDoorStateWithMasterKey()
    {
        open = !open;
        GetComponent<DoorSound>().FlagChangeMurderer();
        GetComponent<DoorSound>().ActiveSoundWave();
        GetComponent<AudioSource>().Play();
        Debug.Log("문을 연다.");
    }

    void Update () 
	{
		if(open) //open == true
		{
			Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
		}
		else
		{
			Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smooth * Time.deltaTime);
		}
	}

}

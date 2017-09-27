using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerOfMidas : MonoBehaviour {

    //움직이는 오브젝트에다 layer moveObject (8번) 적용 꼭 하자.
    
    public GameObject emthyCurserImage;
    public GameObject selecedCurserImage;

    [Range(1.0f,10.0f)]
    public float speed;
    [Range(1.0f,10.0f)]
    public float sensitive;
    [Range(1, 10)]
    public int power;
    public int maxDistance;
    public float  minDistance;

    private GameObject lookObject;
    private bool ishold;

    private void Start()
    {
        lookObject = null;
        ishold = false;
        emthyCurserImage.SetActive(true);
        selecedCurserImage.SetActive(false);
    }

    void Update () {

        MoveInput();
        RotateInput();

        CheckLookObject();
        UseFingerToObject();

    }

    private void CheckLookObject()
    {
        if (ishold)  return;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, maxDistance))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("MoveObject"))
            {
                lookObject = hit.collider.gameObject;
                emthyCurserImage.SetActive(false);
                selecedCurserImage.SetActive(true);
                return;
            }
        }

        lookObject = null;
        emthyCurserImage.SetActive(true);
        selecedCurserImage.SetActive(false);
    }

    private void UseFingerToObject()
    {
        if (Input.GetMouseButtonDown(0))
            ishold = true;

        if (Input.GetMouseButtonUp(0))
            ishold = false;

        if (ishold && lookObject != null)
        {
            Transform tr = Camera.main.transform;
            Rigidbody body = lookObject.GetComponent<Rigidbody>();
            float currDistance = Vector3.Distance(lookObject.transform.position, tr.position + (tr.forward * minDistance));

            if (body != null)
            {
                body.angularVelocity = Vector3.zero;
                body.velocity = Vector3.zero;
            }

            if (currDistance > maxDistance + minDistance)
            {
                ishold = false;
                lookObject = null;
                return;
            }

            lookObject.transform.position = Vector3.Lerp(lookObject.transform.position, tr.position + (tr.forward * minDistance), Time.deltaTime * power);

            //일정 거리 내에 오면 바로앞으로 당겨오기
            //if (Vector3.Distance(lookObject.transform.position, tr.position + (tr.forward * minDistance)) >= minDistance)
            //lookObject.transform.position = Vector3.Lerp(lookObject.transform.position, tr.position + (tr.forward * (minDistance + (power/5))), Time.deltaTime * power);
            //else
            //lookObject.transform.position = tr.position + (tr.forward * minDistance);
        }
    }

    private void MoveInput()
    {
        float deltaX = Input.GetAxis("Horizontal"); 
        float deltaY = Input.GetAxis("Vertical");

        transform.Translate(deltaX * speed, 0, deltaY * speed);
    }

    private void RotateInput()
    {
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        transform.Rotate(-(deltaY * sensitive), deltaX * sensitive, 0);
    }
}

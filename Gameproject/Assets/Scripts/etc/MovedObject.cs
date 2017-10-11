using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovedObject : MonoBehaviour {
    [Range(0.1f, 20.0f)]
    public float weight;
    public Rigidbody body;
    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("MoveObject");
        body = gameObject.GetComponent<Rigidbody>();

        if (body == null)
            body = gameObject.AddComponent<Rigidbody>();

        body.mass = 0.1f;
    }

    /// 무게 
    // 0.1 매우 가볍움 // 매우 빠르게 들리고 매우 밀려남 
    // 1 보통 무게 // 잘 들리고 잘 밀려남
    // 5 조금 무거움 // 완전히 들리지 않지만 어느정도 밀려남
    // 10 많이 무거움 // 들리지 않지만 끌려오며 어느정도 밀쳐짐
    // 20 너무 무거움 // 거의 끌려오지 않고 거의 밀리지도 않음
}

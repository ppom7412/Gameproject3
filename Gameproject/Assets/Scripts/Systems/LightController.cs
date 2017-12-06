using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {
    [SerializeField]
    private Light directionalLight;
    public float minIntensity;
    public float maxIntensity;

	void Start () {
        directionalLight = gameObject.GetComponent<Light>();

        if (directionalLight == null)
        {
            ErrorAdmin.ErrorMessegeFromObject("LightController don't take Light.", "Start()", gameObject);
        }
	}

    public void ChangeToLightIntensity(float _value)
    {
        if (minIntensity > _value)
            directionalLight.intensity = minIntensity;
        else if (maxIntensity < _value)
            directionalLight.intensity = maxIntensity;

        directionalLight.intensity = _value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverItem : MonoBehaviour {

    [SerializeField]
    float verticalSpeed;
    [SerializeField]
    float amplitude;
    [SerializeField]
    float degreesPerSecond;

    Vector3 tempPosition;
    bool grabbed = false;

	// Use this for initialization
	void Start () {
        tempPosition = transform.localPosition;
        grabbed = GetComponent<OVRGrabbable>().isGrabbed;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GetComponent<OVRGrabbable>().isGrabbed)
        {
            transform.Rotate(new Vector3(Time.deltaTime * degreesPerSecond, 0f, Time.deltaTime * degreesPerSecond), Space.World);

            tempPosition.y += Mathf.Sin(Time.fixedTime * Mathf.PI * verticalSpeed) * amplitude;
            transform.localPosition = tempPosition;
        }
	}
}

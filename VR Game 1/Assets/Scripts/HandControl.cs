using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControl : MonoBehaviour {

    [SerializeField]
    OVRInput.Controller controller;

    bool isActivated = false;
    Vector3 oriPos;

    public bool IsActivated
    {
        get { return isActivated; }
        set { isActivated = value; }
    }

    // Use this for initialization
    void Start ()
    {
        oriPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(!IsActivated)
        {
            transform.position = oriPos;
        }
	}

    void FixedUpdate()
    {
        if (IsActivated)
        {
            transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
            transform.localRotation = OVRInput.GetLocalControllerRotation(controller);
        }
    }
}

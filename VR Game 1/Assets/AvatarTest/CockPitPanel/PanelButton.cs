using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PanelButton : MonoBehaviour {

    [SerializeField]
    UnityEvent buttonEvent;
    [SerializeField]
    GameObject menuHandler;

    MenuHandler myMenuHandler;

    bool pushing = false;
    Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        myMenuHandler = menuHandler.GetComponent<MenuHandler>();
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.localPosition.y <= -0.04f && !myMenuHandler.InMenu)
        {
            float x = transform.localPosition.x;
            float z = transform.localPosition.z;
            transform.localPosition = new Vector3(x, 0.0f, z);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            rend.material.color = Color.yellow;
            buttonEvent.Invoke();
            
        }
        if (GetComponent<Rigidbody>().velocity == Vector3.zero && transform.localPosition.y < 0.0f && !pushing)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0.1f, 0f), ForceMode.Impulse);
        }
        if (transform.localPosition.y >= 0.0f)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            float x = transform.localPosition.x;
            float z = transform.localPosition.z;
            transform.localPosition = new Vector3(x, 0.0f, z);
            rend.material.color = Color.green;
        }
    }
    void OnCollisionEnter(Collision collider)
    {
        //GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 0.5f), ForceMode.Impulse);
        pushing = true;
    }
    void OnCollisionExit(Collision collider)
    {
        pushing = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PushButton : MonoBehaviour {

    [SerializeField]
    Slider slider;
    [SerializeField]
    UnityEvent buttonEvent;


    bool pushing;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.z >= 1.5f)
        {
            float x = transform.localPosition.x;
            float y = transform.localPosition.y;
            transform.localPosition = new Vector3(x, y, 1.5f);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            slider.value += 0.007f;
        }
        if(GetComponent<Rigidbody>().velocity == Vector3.zero && transform.localPosition.z > 0 && !pushing)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -0.1f), ForceMode.Impulse);
            slider.value = 0;
        }
        if(transform.localPosition.z < 0.0f)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            float x = transform.localPosition.x;
            float y = transform.localPosition.y;
            transform.localPosition = new Vector3(x, y, 0.0f);
        }
        if(slider.value >= 1)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0.0f);
            slider.value = 0;
            if(buttonEvent != null)
            {
                buttonEvent.Invoke();
            }
        }
	}

    void OnCollisionEnter(Collision collider)
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 0.5f), ForceMode.Impulse);
        pushing = true;
    }
    void OnCollisionExit(Collision collider)
    {
        pushing = false;
    }

    //void OnMouseDown()
    //{
    //    if (transform.localPosition.z < 1.5f)
    //    {
    //        GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 0.5f), ForceMode.Impulse);
    //    }
    //}
}

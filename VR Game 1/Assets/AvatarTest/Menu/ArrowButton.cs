using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowButton : MonoBehaviour {

    [SerializeField]
    Slider slider;
    [SerializeField]
    int amount;
    [SerializeField]
    string setting;

    float timePassed;

    Text txt;

	// Use this for initialization
	void Start () {
        timePassed = 0;
        txt = slider.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        if (transform.localPosition.z >= 1.5f)
        {
            float x = transform.localPosition.x;
            float y = transform.localPosition.y;
            transform.localPosition = new Vector3(x, y, 1.5f);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (timePassed >= 0.5f)
            {
                timePassed = 0;
                slider.value += amount;
            }
        }
        if (GetComponent<Rigidbody>().velocity == Vector3.zero && transform.localPosition.z > 0)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -0.1f), ForceMode.Impulse);
        }
        if (transform.localPosition.z < 0.0f)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            float x = transform.localPosition.x;
            float y = transform.localPosition.y;
            transform.localPosition = new Vector3(x, y, 0.0f);
        }
        if (slider.value > 10)
        {
            slider.value = 10;
        }
        if(slider.value < 0)
        {
            slider.value = 0;
        }
        txt.text = setting + ": " + slider.value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDebugViewer : MonoBehaviour {

    public float weaponRange = 50f;

    public GameObject gun;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 lineOrigin = gun.transform.position;
        Debug.DrawRay(lineOrigin, gun.transform.up * weaponRange, Color.green);
	}
}

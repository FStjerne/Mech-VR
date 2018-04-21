using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCalibration : MonoBehaviour {

    [SerializeField]
    GameObject handAnchor;

	// Update is called once per frame
	void Update () {
        transform.position = handAnchor.transform.position;
        transform.rotation = handAnchor.transform.rotation;
	}
}

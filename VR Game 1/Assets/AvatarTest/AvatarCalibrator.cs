using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarCalibrator : MonoBehaviour {

    [SerializeField]
    GameObject LeftHand;
    [SerializeField]
    GameObject LeftHandAvatar;

	
	// Update is called once per frame
	void Update () {
        transform.position += LeftHand.transform.position - LeftHandAvatar.transform.position;
	}
}

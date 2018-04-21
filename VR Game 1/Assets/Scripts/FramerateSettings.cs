using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateSettings : MonoBehaviour {

    [SerializeField]
    int targetCount = 60;

	// Use this for initialization
	void Start () {
        QualitySettings.vSyncCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(targetCount != Application.targetFrameRate)
        {
            Application.targetFrameRate = targetCount;
        }
	}
}

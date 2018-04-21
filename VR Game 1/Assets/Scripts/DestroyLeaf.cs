using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLeaf : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 10.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

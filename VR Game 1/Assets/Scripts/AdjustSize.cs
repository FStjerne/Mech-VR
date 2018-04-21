using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSize : MonoBehaviour {
    public GameObject theGameObject;
    public float newSize;
	// Use this for initialization
	void Start () {
        newScale(theGameObject, newSize);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void newScale(GameObject theGameObject, float newSize)
    {
        float size = theGameObject.GetComponent<Renderer>().bounds.size.y;

        Vector3 rescale = theGameObject.transform.localScale;

        rescale.y = newSize * rescale.y / size;

        theGameObject.transform.localScale = rescale;

    }
}

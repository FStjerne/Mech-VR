using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrab : MonoBehaviour {
    [SerializeField]
    private GameObject leaves;
    private OVRGrabbable grab;
    [SerializeField]
    private List<GameObject> gameObjects;
    [SerializeField]
    private Transform leafpos;
    private bool instantiated = false;
	// Use this for initialization

    
    public void OnMouseDown()
    {
        if (leaves)
        {
            Instantiate(leaves, leafpos.transform.TransformPoint(leaves.transform.position), leafpos.rotation);
        }

        foreach (GameObject go in gameObjects)
        {
            Destroy(go);
        }
    }

    public void Grabbed()
    {
        if (leaves && !instantiated)
        {
            instantiated = true;
            foreach (GameObject go in gameObjects)
            {
                Destroy(go);
            }

            Instantiate(leaves, leafpos.transform.TransformPoint(leaves.transform.position), transform.rotation);
        }
    }

}

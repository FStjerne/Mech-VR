using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {
    [SerializeField]
    private GameObject destroyedWall;
    private OVRInput.Controller controller;


    void Update()
    {
        //OVRInput.Axis1D.PrimaryHandTrigger;
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) == 1f)
        {
            if (destroyedWall)
            {
                Instantiate(destroyedWall, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }


    public void OnMouseDown()
    {
        if (destroyedWall)
        {
            Instantiate(destroyedWall, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}

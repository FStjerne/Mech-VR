using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechHandControls : MonoBehaviour {

    [SerializeField]
    public GameObject LeftHandController;
    [SerializeField]
    public GameObject RightHandController;
    [SerializeField]
    public GameObject CockPitLeftHand;
    [SerializeField]
    public GameObject CockPitRightHand;
    [SerializeField]
    public GameObject MechRightHand;
    [SerializeField]
    public GameObject MechLeftHand;
    [SerializeField]
    public float SizeMultiplier;

    private Quaternion OriginHandRotation;
    private bool MechControlReady = true;
    private OVRGrabbable LeftController;
    private OVRGrabbable RightController;
    private OVRGrabber LeftHand;
    private OVRGrabber RightHand;
    private Collider RightHandCollider;
    private Collider LeftHandCollider;


    // Use this for initialization
    void Start ()
    {
        if(SizeMultiplier == 0)
        {
            SizeMultiplier = 1;
        }
		if(CockPitLeftHand == null || CockPitRightHand == null || 
            MechLeftHand == null || MechLeftHand == null)
        {
            MechControlReady = false;
        }
        if(LeftHandController.GetComponent("OVRGrabber") != null)
        {
            LeftHand = (OVRGrabber)LeftHandController.GetComponent("OVRGrabber");
            LeftHandCollider = LeftHand.GetComponent<Collider>();
        }
        if (RightHandController.GetComponent("OVRGrabber") != null)
        {
            RightHand = (OVRGrabber)RightHandController.GetComponent("OVRGrabber");
            RightHandCollider = RightHand.GetComponent<Collider>();
        }
        OriginHandRotation = CockPitLeftHand.transform.localRotation;

        if (CockPitLeftHand.GetComponent("OVRGrabbable") != null)
        {
            LeftController = (OVRGrabbable)CockPitLeftHand.GetComponent("OVRGrabbable");
        }
        if (CockPitRightHand.GetComponent("OVRGrabbable") != null)
        {
            RightController = (OVRGrabbable)CockPitRightHand.GetComponent("OVRGrabbable");
        }
        if(RightController == null || LeftController == null || RightHand == null || LeftHand == null)
        {
            MechControlReady = false;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(MechControlReady == true)
        {
            if (RightController.CollidingWithController && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0)
            {
                grabbed(RightController, RightHand);
            }
            else
            {
                StationaryRight(CockPitRightHand);
            }
            if (LeftController.CollidingWithController && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0)
            {
                grabbed(LeftController, LeftHand);
            }
            else
            {
                StationaryLeft(CockPitLeftHand);
            }

            MechLeftHand.transform.localPosition = CockPitLeftHand.transform.localPosition * SizeMultiplier;
            MechLeftHand.transform.localRotation = CockPitLeftHand.transform.localRotation;

            MechRightHand.transform.localPosition = CockPitRightHand.transform.localPosition * SizeMultiplier;
            MechRightHand.transform.localRotation = CockPitRightHand.transform.localRotation;

        }
    }
    
    private void grabbed(OVRGrabbable Controller, OVRGrabber Hand)
    {
        Controller.transform.localPosition = Hand.gameObject.transform.localPosition;
        Controller.transform.localRotation = Hand.gameObject.transform.localRotation;
    }

    private void StationaryLeft(GameObject Hand)
    {
        Hand.transform.localPosition = new Vector3(0.3f,-0.5f,0.4f);
        Hand.transform.localRotation = OriginHandRotation;
    }
    private void StationaryRight(GameObject Hand)
    {
        Hand.transform.localPosition = new Vector3(-0.3f, -0.5f, 0.4f);
        Hand.transform.localRotation = OriginHandRotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechShooter : MonoBehaviour {

    public float gunDamage = 1;
    public float RLDamage = 3;
    public float fireRateMG = 0.01f;
    public float fireRateRL = 1f;
    public float weaponRange = 50f;
    public float gravityRange = 5f;
    public Transform gunEnd;
    public Transform gravityEnd;
    public Transform RLEnd;
    public bool rightHand;
    public GameObject bullet;
    public GameObject rocketGrenade;
    public float bulletSpeedTest = 100;
    public float rocketSpeed = 100;
    public bool gunWorks = false;
    public GameObject rocketLauncher;
    public GameObject gravityGun;
    public List<GameObject> arsenal = new List<GameObject>();
    public string gunName;

    private float nextFireMG;
    private float nextFireRL;
    private int amountOfGuns;
    private int currentGun;
    private bool holdingButtonRight = false;
    private bool holdingButtonLeft = false;
    private LineRenderer laserLine;
    private GameObject gravityTarget;
    private bool gravityObjectGrabbed = false;
    private Collider gravityTargetCollider;


	// Use this for initialization
	void Start ()
    {
        if(GetComponent<LineRenderer>() != null)
        {
            laserLine = GetComponent<LineRenderer>();
            laserLine.enabled = false;
        }
        if(bullet != null)
        {
            gunWorks = true;
            arsenal.Add(this.gameObject);
        }
        if(bulletSpeedTest == 0)
        {
            bulletSpeedTest = 1;
        }
        if(gravityGun != null)
        {
            arsenal.Add(gravityGun);
            gravityGun.SetActive(false);
        }
        if(rocketLauncher != null)
        {
            arsenal.Add(rocketLauncher);
            rocketLauncher.SetActive(false);
        }
        amountOfGuns = arsenal.Count;
        if(amountOfGuns > 0)
        {
            currentGun = 1;
            gunName = "Light Machine Gun";
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (currentGun)
        {
            case 1:
                MachineGun();
                break;
            case 2:
                GravityGun();
                break;
            case 3:
                RocketLauncher();
                break;
        }

        if (rightHand)
        {
            if (OVRInput.Get(OVRInput.Button.One) && !holdingButtonRight)
            {
                holdingButtonRight = true;

                currentGun++;
                if (currentGun > amountOfGuns)
                {
                    currentGun = 1;
                }
                switch (currentGun)
                {
                    case 1:
                        gunName = "Light Machine Gun";
                        rocketLauncher.SetActive(false);
                        break;
                    case 2:
                        gunName = "Gravity Gun";
                        gravityGun.SetActive(true);
                        gravityTarget = null;
                        gravityTargetCollider = null;
                        laserLine.enabled = true;
                        break;
                    case 3:
                        gunName = "Rocket Launcher";
                        gravityGun.SetActive(false);
                        laserLine.enabled = false;
                        rocketLauncher.SetActive(true);
                        break;
                }
            }
            if(OVRInput.Get(OVRInput.Button.One) != true && holdingButtonRight)
            {
                holdingButtonRight = false;
            }
        }
        else
        {
            if (OVRInput.Get(OVRInput.Button.Three) && !holdingButtonLeft)
            {
                holdingButtonLeft = true;


                currentGun++;
                if (currentGun > amountOfGuns)
                {
                    currentGun = 1;
                }
                switch (currentGun)
                {
                    case 1:
                        gunName = "Light Machine Gun";
                        rocketLauncher.SetActive(false);
                        break;
                    case 2:
                        gunName = "Gravity Gun";
                        gravityGun.SetActive(true);
                        gravityTarget = null;
                        gravityTargetCollider = null;
                        laserLine.enabled = true;
                        break;
                    case 3:
                        gunName = "Rocket Launcher";
                        gravityGun.SetActive(false);
                        laserLine.enabled = false;
                        rocketLauncher.SetActive(true);
                        break;
                }
            }
            if (OVRInput.Get(OVRInput.Button.Three) != true && holdingButtonLeft)
            {
                holdingButtonLeft = false;
            }
        }
	}

    void MachineGun()
    {
        nextFireMG += Time.deltaTime;
        if (gunWorks)
        {
            if (rightHand)
            {
                if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0 && nextFireMG > fireRateMG)
                {
                    nextFireMG = 0;
                    bullet.transform.rotation = transform.rotation;
                    bullet.transform.position = gunEnd.transform.position;
                    BulletTime bulletTime = bullet.GetComponent<BulletTime>();
                    bulletTime.bulletSpeed = bulletSpeedTest;
                    bulletTime.damage = gunDamage;
                    bulletTime.WeaponRange = weaponRange;
                    bulletTime.direction = Vector3.up;
                    Instantiate(bullet);
                }
            }
            else
            {
                if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0 && nextFireMG > fireRateMG)
                {
                    nextFireMG = 0;
                    bullet.transform.rotation = transform.rotation;
                    bullet.transform.position = gunEnd.transform.position;
                    BulletTime bulletTime = bullet.GetComponent<BulletTime>();
                    bulletTime.bulletSpeed = bulletSpeedTest;
                    bulletTime.damage = gunDamage;
                    bulletTime.WeaponRange = weaponRange;
                    bulletTime.direction = Vector3.up;
                    Instantiate(bullet);

                }
            }
        }
    }
    
    void RocketLauncher()
    {
        nextFireRL += Time.deltaTime;
        if (rightHand)
        {
            if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0 && nextFireRL > fireRateRL)
            {
                nextFireRL = 0;
                rocketGrenade.transform.rotation = transform.rotation;
                rocketGrenade.transform.position = RLEnd.transform.position;
                RocketGrenade settings = rocketGrenade.GetComponent<RocketGrenade>();
                settings.SelfPropelling = false;
                settings.rocketSpeed = rocketSpeed/2;
                settings.rocketDirection = Vector3.up;
                settings.timer = 3;
                settings.damage = RLDamage;
                Instantiate(rocketGrenade);
            }
        }
        else
        {
            if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0 && nextFireRL > fireRateRL)
            {
                nextFireRL = 0;
                rocketGrenade.transform.rotation = transform.rotation;
                rocketGrenade.transform.position = RLEnd.transform.position;
                RocketGrenade settings = rocketGrenade.GetComponent<RocketGrenade>();
                settings.SelfPropelling = true;
                settings.rocketSpeed = rocketSpeed;
                settings.rocketDirection = Vector3.up;
                settings.timer = 10;
                settings.damage = RLDamage;
                Instantiate(rocketGrenade);
            }
        }
    }

    void GravityGun()
    {
        Vector3 rayOrigin = transform.position;
        RaycastHit hit;

        laserLine.SetPosition(0, gunEnd.position);

        if (Physics.Raycast(rayOrigin, gunEnd.up, out hit, gravityRange))
        {
            laserLine.SetPosition(1, hit.point);

            if(hit.collider.GetComponent<Shootable>() != null)
            {
                if(hit.collider.attachedRigidbody.mass < 10)
                {
                    gravityTarget = hit.collider.gameObject;
                }
            }
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (gunEnd.up * gravityRange));
            if(gravityTarget != null)
            {
                gravityTarget = null;
                if(gravityTargetCollider != null)
                {
                    gravityTargetCollider.attachedRigidbody.velocity = Vector3.zero;
                    gravityTargetCollider.attachedRigidbody.useGravity = true;
                    gravityTargetCollider = null;
                }
            }
        }
        if (rightHand)
        {
            if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0)
            {
                if(gravityTarget != null)
                {
                    gravityObjectGrabbed = true;
                    if(gravityTarget.GetComponent<Collider>() != null)
                    {
                        gravityTargetCollider = gravityTarget.GetComponent<Collider>();
                    }
                    if(gravityTarget.GetComponent<TreeGrab>() != null)
                    {
                        gravityTarget.GetComponent<TreeGrab>().Grabbed();
                    }
                }
            }
            else
            {
                if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) == 0)
                {
                    if (gravityObjectGrabbed)
                    {
                        gravityTargetCollider.attachedRigidbody.useGravity = true;
                        gravityTargetCollider.attachedRigidbody.AddForce(gunEnd.up * 50, ForceMode.Impulse);
                        gravityObjectGrabbed = false;
                        gravityTargetCollider = null;
                    }
                }
            }
        }
        else
        {
            if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0)
            {
                if (gravityTarget != null)
                {
                    gravityObjectGrabbed = true;
                    if (gravityTarget.GetComponent<Collider>() != null)
                    {
                        gravityTargetCollider = gravityTarget.GetComponent<Collider>();
                    }
                }
            }
            else
            {
                if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) == 0)
                {
                    if (gravityObjectGrabbed)
                    {
                        gravityTargetCollider.attachedRigidbody.useGravity = true;
                        gravityTargetCollider.attachedRigidbody.AddForce(gunEnd.up * 50, ForceMode.Impulse);
                        gravityObjectGrabbed = false;
                        gravityTargetCollider = null;
                    }
                }
            }
        }
        if(gravityTarget != null)
        {
            if (gravityObjectGrabbed)
            {
                gravityTargetCollider.attachedRigidbody.velocity = Vector3.zero;
                gravityTarget.transform.position = gravityEnd.position;
                gravityTargetCollider.attachedRigidbody.useGravity = false;
            }
            else
            {
                gravityTargetCollider.attachedRigidbody.useGravity = true;
            }
        }
        

    }
}

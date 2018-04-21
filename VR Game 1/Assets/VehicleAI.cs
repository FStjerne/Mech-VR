using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAI : MonoBehaviour {

    public GameObject target;
    public GameObject turret;
    public GameObject bullet;
    public GameObject gunEnd;
    public GameObject explosion;
    public Light bulletflash;
    public float weaponRange;
    public float bulletSpeedTest;
    public float gunDamage;
    public float maxAngle;
    public float rotationSpeed;
    public float fireRate;
    public bool heavyWeaponry;
    public Vector3 Offset;
    public bool working = true;
    private bool shooting = false;
    private float nextFire;

	// Use this for initialization
	void Start ()
    {
        if (target == null || turret == null)
        {
            working = false;
        }
        else
        {
            working = true;
        }
        if (maxAngle == 0)
        {
            maxAngle = 45;
        }
        if(rotationSpeed == 0)
        {
            rotationSpeed = 1;
        }
        if(fireRate == 0)
        {
            fireRate = 5;
        }
        if(weaponRange == 0)
        {
            weaponRange = 100000;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (working)
        {
            if (!shooting)
            {
                if(target != null)
                {
                    Vector3 targetDir = target.transform.position - turret.transform.position;
                    float step = rotationSpeed * Time.deltaTime;
                    Vector3 newDir = Vector3.RotateTowards(turret.transform.forward, targetDir, step, 0.0f);

                    turret.transform.rotation = Quaternion.LookRotation(newDir);

                    RaycastHit hit;
                    if (Physics.Raycast(turret.transform.position, turret.transform.forward, out hit, weaponRange))
                    {
                        if (hit.collider.GetComponent<Shootable>() != null)
                        {
                            if (target.transform.position == hit.collider.transform.position)
                            {
                                Shoot(hit);
                            }
                        }
                    }
                }
            }
            else
            {
                nextFire += Time.deltaTime;
                if(bulletflash != null)
                {
                    if (bulletflash.intensity > 0)
                    {
                        bulletflash.intensity -= (nextFire * 20);
                        if (bulletflash.intensity < 0)
                        {
                            bulletflash.intensity = 0;
                        }
                    }
                }
                if(nextFire > fireRate)
                {
                    nextFire = 0;
                    shooting = false;
                }
            }
        }
	}

    public void Shoot(RaycastHit hit)
    {
        shooting = true;
        if(bulletflash != null)
        {
            bulletflash.intensity = 10;
        }

        if(bullet == null && gunEnd == null)
        {
            hit.collider.gameObject.GetComponent<Shootable>().Damage(gunDamage);
            if (heavyWeaponry)
            {
                if(explosion != null)
                {
                    explosion.transform.position = hit.point;
                    Instantiate(explosion);
                }
            }
        }
        else
        {
            bullet.transform.rotation = gunEnd.transform.rotation;
            bullet.transform.position = gunEnd.transform.position;
            RocketGrenade settings = bullet.GetComponent<RocketGrenade>();
            settings.SelfPropelling = true;
            settings.rocketSpeed = bulletSpeedTest;
            settings.timer = 10;
            settings.damage = gunDamage;
            Instantiate(bullet);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGrenade : MonoBehaviour {

    public float damage;
    public float explosionRange;
    public float explosionForce;
    public bool SelfPropelling;
    public float rocketSpeed;
    public Vector3 rocketDirection;
    public float timer;
    public Explosion explosion;
    public GameObject explosionEffect;
    public GameObject rocketTrailEffect;
    public bool exploded = false;
    // Use this for initialization

    private float timeSinceDeployment;

	void Start ()
    {
		if(timer == 0)
        {
            timer = 2;
        }
        rocketDirection *= rocketSpeed;
        if(GetComponent<Rigidbody>() != null)
        {
            Rigidbody rocketrigid = GetComponent<Rigidbody>();
            if (SelfPropelling)
            {
                if(rocketTrailEffect != null)
                {
                    if(rocketTrailEffect.GetComponent<RocketTrailProperties>() != null)
                    {
                        RocketTrailProperties trailproperties = rocketTrailEffect.GetComponent<RocketTrailProperties>();
                        trailproperties.direction = rocketDirection;
                        trailproperties.rocket = this;
                    }
                    rocketTrailEffect.transform.position = transform.position;
                    Instantiate(rocketTrailEffect);
                }
                rocketrigid.useGravity = false;
            }
            else
            {
                rocketrigid.useGravity = true;
                rocketrigid.AddForce(transform.up * rocketSpeed, ForceMode.Impulse);
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeSinceDeployment += Time.deltaTime;
        if (SelfPropelling)
        {
            transform.Translate(rocketDirection * Time.deltaTime);
        }
        if(timeSinceDeployment > timer)
        {
            Explode();
        }
	}
    
    void Explode()
    {
        exploded = true;
        explosion.Explode(explosionForce, damage, explosionRange, explosionEffect);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<Explosion>() == null)
        {
            if (SelfPropelling)
            {
                if(timeSinceDeployment > 0.1f)
                {
                    Explode();
                }
            }
        }
    }
}

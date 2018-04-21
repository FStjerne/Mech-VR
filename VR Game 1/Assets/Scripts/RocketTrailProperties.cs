using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTrailProperties : MonoBehaviour {

    public RocketGrenade rocket;
    public Vector3 direction;
    public float timer;
    public ParticleEffectTimer effectTimer;

    private float timeSinceDeployment;
    private bool addedEffectTimer = false;

	// Use this for initialization
	void Start ()
    {
        timer = 0;
		if(rocket != null)
        {
            direction = Vector3.up;
            transform.rotation = rocket.transform.rotation;
            timer = rocket.timer;
        }
        effectTimer.enabled = false;
        timeSinceDeployment = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeSinceDeployment += Time.deltaTime;
        if(timeSinceDeployment > timer || rocket.exploded == true)
        {
            if(addedEffectTimer == false)
            {
                Explode();
            }
        }
        else if(addedEffectTimer == false)
        {
            transform.Translate(direction * rocket.rocketSpeed * Time.deltaTime);
        }
	}

    public void Explode()
    {
        addedEffectTimer = true;
        effectTimer.enabled = true;
        direction = Vector3.zero;
        effectTimer.timer = 5;
    }
}

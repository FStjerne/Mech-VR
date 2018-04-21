using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTime : MonoBehaviour {

    public Vector3 direction;
    public float bulletSpeed;
    public float damage;
    public float WeaponRange;

    private Vector3 originPosition;


	// Use this for initialization
	void Start ()
    {
        if(WeaponRange == 0)
        {
            WeaponRange = 1;
        }
        originPosition = transform.position;
        direction *= bulletSpeed;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(direction*Time.deltaTime);

        if(Vector3.Distance(originPosition,transform.position) > WeaponRange)
        {
            Destroy(this.gameObject);
        }
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<Shootable>() != null)
        {
            Shootable box = other.gameObject.GetComponent<Shootable>();
            box.Damage(damage);
        }
        Destroy(this.gameObject);
    }

    void OnCollisionExit(Collision other)
    {

    }
}

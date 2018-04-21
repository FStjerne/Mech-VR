using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    private List<Shootable> affectedObjects = new List<Shootable>();

    public void Explode(float force, float damage, float radius, GameObject Explosion)
    {
        foreach (Shootable obj in affectedObjects)
        {
            obj.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, 1, ForceMode.Impulse);
            obj.Damage(damage - ((Vector3.Distance(this.transform.position, obj.transform.position) / radius) * damage));
        }

        Instantiate(Explosion,this.transform.position,Quaternion.Euler(new Vector3(-90,0,0)));
        Destroy(gameObject.GetComponentInParent<RocketGrenade>().gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Shootable>() != null)
        {
            affectedObjects.Add(other.GetComponent<Shootable>());
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Shootable>() != null)
        {
            affectedObjects.Remove(other.GetComponent<Shootable>());
        }
    }
}

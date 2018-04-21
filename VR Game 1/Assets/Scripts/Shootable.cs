using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    public GameObject GrabPoint;
    public float currentHealth = 3f;
    public void Damage(float damageAmount)
    {
        if(GetComponent<MechMovement>() != null)
        {
            GetComponent<MechMovement>().Damage((int)damageAmount);
        }
        if(gameObject.tag != "Player")
        {
            currentHealth -= damageAmount;
        }
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}

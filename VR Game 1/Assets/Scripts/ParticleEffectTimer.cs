using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectTimer : MonoBehaviour
{
    public float timer;
    private float timeSinceDeployment = 0;
    // Use this for initialization
    void Start()
    {
        if(timer == 0)
        {
            timer = 4;
        }
        timeSinceDeployment = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceDeployment += Time.deltaTime;
        if(timeSinceDeployment > timer)
        {
            Destroy(this.gameObject);
        }
    }
}

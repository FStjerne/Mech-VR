using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovementTest : MonoBehaviour {

    [SerializeField]
    private Transform[] goals;
    private Animator animator;
    private int destPoint = 0;
    private NavMeshAgent agent;
    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);
    void Start () {
        //animator = gameObject.GetComponent<Animator>();
        //animator.SetFloat("Direction 0", 0f);
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GotoNextPoint();
    }
	

	void Update () {
       
        //if (goal.position == GetComponent<GameObject>().transform.position)
        //{
        //    animator.SetBool("move", false);
        //}
        CanSeePlayer();
        
            
        if(agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }

    void GotoNextPoint()
    {
        if (goals.Length == 0)
            return;

        agent.destination = goals[destPoint].position;

        destPoint = (destPoint + 1) % goals.Length;
    }

    void CanSeePlayer()
    {
        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        for (var i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, 500))
            {
                var player = hit.collider.GetComponent<GameObject>();
                if (player)
                {
                    
                }
            }
            direction = stepAngle * direction;
        }
    }
}

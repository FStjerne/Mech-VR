using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class EnemySight : MonoBehaviour
    {
        private Animator animator;
        public NavMeshAgent agent;
        public ThirdPersonCharacter character;

        public enum State
        {
            PATROL,
            CHASE,
            INVESTIGATE,
            SHOOT
        }

        public State state;
        private bool timed = false;
        public float maxDist;
        private bool alive;


        //Variables for patrolling
        public GameObject[] waypoints = new GameObject[20];
        private int waypointInd;
        public float patrolSpeed = 1.5f;

        //Variables for chasing
        public float chaseSpeed = 3f;
        public GameObject target;

        //Variables for investigate
        private Vector3 investigateSpot;
        private float timer = 0;
        public float investigateWait = 10;

        //Variables for sight
        public float heightMultiplier;
        public float sightDist = 10;

        //Variables for shooting
        public float fireRateMG = 1.0f;
        private float nextFireMG;
        public float bulletSpeedTest = 10;
        public GameObject bullet;
        public Transform gunEnd;
        public float weaponRange = 50f;
        public float gunDamage = 3;


        // Use this for initialization
        void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
            waypointInd = Random.Range(0, waypoints.Length);

            alive = true;

            state = EnemySight.State.PATROL;

            heightMultiplier = 1.36f;

            StartCoroutine("FSM");
        }

        IEnumerator FSM()
        {
            while (alive)
            {
                switch (state)
                {
                    case State.PATROL:
                        Patrol();
                        break;
                    case State.CHASE:
                        Chase();
                        break;
                    case State.INVESTIGATE:
                        Investigate();
                        break;
                    case State.SHOOT:
                        Shoot();
                        break;
                }
                yield return null;
            }
        }

        void Patrol()
        {
            animator.SetBool("Hostile", false);
            animator.SetFloat("Speed", 0.5f);
            agent.speed = patrolSpeed;
            if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 2)
            {
                agent.SetDestination(waypoints[waypointInd].transform.position);
                character.Move(agent.desiredVelocity, false, false);
            }
            else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2)
            {
                waypointInd = Random.Range(0, waypoints.Length);
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }
        }

        void Chase()
        {
            animator.SetBool("move", true);
            animator.SetBool("Hostile", true);
            animator.SetFloat("Speed", 1f);
            agent.speed = chaseSpeed;
            agent.SetDestination(target.transform.position);
            character.Move(agent.desiredVelocity, false, false);
        }

        void Investigate()
        {
            animator.SetBool("Hostile", false);
            animator.SetBool("move", false);
            animator.SetFloat("Speed", -0.2f);
            timer += Time.deltaTime;
            agent.SetDestination(this.transform.position);
            character.Move(Vector3.zero, false, false);
            transform.LookAt(investigateSpot);
            if (timer >= investigateWait)
            {
                state = EnemySight.State.PATROL;
                timer = 0;
            }
        }

        void Shoot()
        {
            nextFireMG += Time.deltaTime;
            animator.SetBool("move", false);
            animator.SetFloat("Speed", 0f);
            agent.SetDestination(this.transform.position);
            character.Move(Vector3.zero, false, false);
            transform.LookAt(target.transform.position + new Vector3(0,-1, 0));
            if (target != null)
            {
                if (Vector3.Distance(transform.position, target.transform.position) <= 5 && nextFireMG > fireRateMG)
                {
                    nextFireMG = 0;
                    bullet.transform.rotation = gunEnd.transform.rotation;
                    bullet.transform.position = gunEnd.transform.position;
                    BulletTime bulletTime = bullet.GetComponent<BulletTime>();
                    bulletTime.bulletSpeed = bulletSpeedTest;
                    bulletTime.damage = gunDamage;
                    bulletTime.WeaponRange = weaponRange;
                    bulletTime.direction = Vector3.up;
                    Instantiate(bullet);
                }
            }
            if (target != null)
            {
                if (Vector3.Distance(transform.position, target.transform.position) >= 5)
                {
                    state = EnemySight.State.CHASE;
                }
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
                target = coll.gameObject;
                state = EnemySight.State.CHASE;
                investigateSpot = coll.gameObject.transform.position;
            }
        }

        void FixedUpdate()
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    target = hit.collider.gameObject;
                    state = EnemySight.State.CHASE;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    target = hit.collider.gameObject;
                    state = EnemySight.State.CHASE;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    target = hit.collider.gameObject;
                    state = EnemySight.State.CHASE;
                }
            }
            if (target != null)
            {
                if (Vector3.Distance(transform.position, target.transform.position) >= maxDist)
                {
                    timed = true;

                    if (timed)
                        timer += Time.deltaTime;

                    if (timer >= investigateWait)
                    {
                        state = EnemySight.State.INVESTIGATE;
                        if (target != null)
                        {
                            investigateSpot = target.transform.position;
                            target = null;
                            timer = 0;
                            timed = false;
                        }
                    }
                }
            }
            if (target != null)
            {
                if (Vector3.Distance(transform.position, target.transform.position) <= 5)
                {
                    state = EnemySight.State.SHOOT;
                }
            }
        }
    }
}



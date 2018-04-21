using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    private List<GameObject> enemies = new List<GameObject>();
    public GameObject[] spawnPoints;
    [SerializeField]
    private GameObject enemy;
    private int spawnpointInd;
    private bool instantiated = false;
    private float deltaTime;
    private float spawnTime = 60f;
    public int enemyCounter = 5;
    public bool spawnOnTime = false;

    public List<GameObject> Enemies
    {
        get
        {
            return enemies;
        }

        set
        {
            enemies = value;
        }
    }

    void Start() {
        
    }
	// Update is called once per frame
	void Update () {
        if (instantiated && spawnOnTime)
        {
            deltaTime = Time.deltaTime;
            if (deltaTime > spawnTime)
            {
                SpawnEnemy();
                deltaTime = 0;
            }
        }
	}

    void OnTriggerEnter(Collider coll)
    {
        if (instantiated == false)
        {
            instantiated = true;
            for (int i = 0; i < enemyCounter; i++)
            {
                spawnpointInd = Random.Range(0, spawnPoints.Length);
                Enemies.Add(enemy);
                enemy.transform.position = spawnPoints[spawnpointInd].transform.position;
                Instantiate(enemy);
             
            }
        }
    }

    void SpawnEnemy()
    {
        for(int i = 0; i < enemyCounter; i++)
        {
            spawnpointInd = Random.Range(0, spawnPoints.Length);
            Enemies.Add(enemy);
            enemy.transform.position = spawnPoints[spawnpointInd].transform.position;
            Instantiate(enemy);          
        }
        enemyCounter += 5;
    }
}

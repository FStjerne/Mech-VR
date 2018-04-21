using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTest : MonoBehaviour {


    [SerializeField]
    GameObject player = null;
    [SerializeField]
    GameObject objective1 = null;
    [SerializeField]
    GameObject objective2 = null;
    [SerializeField]
    GameObject objective3 = null;
    [SerializeField]
    Text objectiveText = null;



    Collider[] hitColliders;
    float radarSize = 2.5f;
    float secondCheck;
    int currentmission;
    int objective1Time = 60;
    int objective3Time = 10;
    int enemiesRemaining = 10;

    bool objective1Active = false;
    bool objective2Active = false;
    bool objective3Active = false;

    // Use this for initialization
    void Start ()
    {
        currentmission = 1;
        objective1.SetActive(false);
        objective2.SetActive(false);
        objective3.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (currentmission == 1)
        {
            hitColliders = Physics.OverlapSphere(objective1.transform.position, radarSize);

            objective1.SetActive(true);
            objectiveText.text = "Go to the Objective";

            if(objective1Active == true)
            {
                objectiveText.text = "Stay Alive for 1 minute \n " + objective1Time + " Seconds remaining";

                secondCheck += Time.deltaTime;
                if(secondCheck >= 1)
                {
                    objective1Time--;
                    secondCheck = 0;
                }
                else if(objective1Time <= 0)
                {                    
                    objective1.SetActive(false);
                    currentmission = 2;

                }
            }

            else if (hitColliders.Length != 0)
            {
                for (int i = 0; i <= hitColliders.Length - 1; i++)
                {
                    if (hitColliders[i].tag == "Player")
                    {
                        objective1Active = true;
                    }
                }
            }
        }

        else if (currentmission == 2)
        {
            objective2.SetActive(true);
            hitColliders = Physics.OverlapSphere(objective2.transform.position, radarSize);

            objectiveText.text = "Go to the 2nd Objective";

            if (objective2Active == true)
            {
                    objective2.SetActive(false);
                    currentmission = 3;
            }

            else if (hitColliders.Length != 0)
            {
                for (int i = 0; i <= hitColliders.Length - 1; i++)
                {
                    if (hitColliders[i].tag == "Player")
                    {
                        objective2Active = true;
                    }
                }
            }
        }
        else if (currentmission == 3)
        {
            hitColliders = Physics.OverlapSphere(objective3.transform.position, radarSize);

            objective3.SetActive(true);
            objectiveText.text = "Go to the 3rd Objective";

            if (objective3Active == true)
            {
                objectiveText.text = "Kill all the enemies \n " + objective3.GetComponent<Spawn>().Enemies.Count + " Enemies remaining";
                
                if (objective3.GetComponent<Spawn>().Enemies.Count <= 0)
                {
                    objectiveText.text = "Accomplished";
                    objective3.SetActive(false);
                    currentmission = 4;

                }
            }

            else if (hitColliders.Length != 0)
            {
                for (int i = 0; i <= hitColliders.Length - 1; i++)
                {
                    if (hitColliders[i].tag == "Player")
                    {
                        objective3Active = true;
                    }
                }
            }
        }
    }
}
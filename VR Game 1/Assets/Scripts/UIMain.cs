using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoBehaviour {

    [SerializeField]
    Text hpText = null;
    [SerializeField]
    Slider hpBar = null;
    [SerializeField]
    Image hpFill = null;
    [SerializeField]
    Text shieldText = null;
    [SerializeField]
    Slider shieldBar = null;
    [SerializeField]
    Image shieldFill = null;
    [SerializeField]
    GameObject rotCamOrigin = null;
    [SerializeField]
    Slider rotBar = null;
    [SerializeField]
    GameObject compasEnemyDot = null;
    [SerializeField]
    GameObject compasLesserEnemyDot = null;
    [SerializeField]
    GameObject compasObjectiveDot = null;
    [SerializeField]
    int radarSize = 0;
    [SerializeField]
    int radarSensitivity = 0;
    [SerializeField]
    int enemyRadarSensitivity = 20;

    int hp = 1000;
    int shield = 1000;
    float secondCheck = 0;
    int arrayPoint = 0;
    Collider[] hitColliders;
    public List<GameObject> dotList;
    public float updateInterval = 0.5F;


    //temp
    float angle;


    private float accum = 0; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval
                            // Use this for initialization

    // Use this for initialization
    void Start() {
        hpBar.value = 100;
        timeleft = updateInterval;
    }

    // Update is called once per frame
    void Update() {

        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        secondCheck += Time.deltaTime;

        if (shield > 1000)
        {
            shield = 1000;
        }
        if(hp > 1000)
        {
            hp = 1000;
        }
        if (secondCheck >= 3)
        {
            shield += 100;
            secondCheck = 0;
        }
        if (hp <= 0)
        {
            Destroy(rotCamOrigin);
        }
        if (timeleft <= 0.0)
        {

            //if (shield > 0)
            //{
            //    shield--;
            //}
            //else if (hp > 0 && shield == 0)
            //{
            //    hp--;
            //}
            //else if (hp <= 0)
            //{
            //    hp = 100;
            //    shield = 100;
            //}

            if (dotList.Count != 0)
            {
                foreach (GameObject go in dotList)
                {
                    Destroy(go);
                }
            }

            dotList.Clear();

            hitColliders = Physics.OverlapSphere(rotCamOrigin.transform.position, radarSize);

            if (hitColliders.Length != 0)
            {
                for (int i = 0; i <= hitColliders.Length - 1; i++)
                {
                    if (hitColliders[i].tag == "Enemy")
                    {
                        Vector2 v2 = new Vector2(rotCamOrigin.transform.position.x - hitColliders[i].transform.position.x, rotCamOrigin.transform.position.z - hitColliders[i].transform.position.z);
                        
                        if(v2.magnitude < enemyRadarSensitivity)
                        {
                            Vector2 v1 = new Vector2(rotCamOrigin.transform.forward.normalized.x, rotCamOrigin.transform.forward.normalized.z);


                            float distanceFormula = 0.3f / (1 + (1 * v2.magnitude / radarSensitivity));

                            //normalize
                            Vector2 v2norm = new Vector2(v2.normalized.x * -1, v2.normalized.y * -1);

                            float camrot = rotCamOrigin.transform.localRotation.eulerAngles.y;

                            float vdot = (v1.x * v2norm.x + v1.y * v2norm.y) * 360;

                            if (camrot > 180)
                            {
                                angle = Mathf.Atan2(v1.x, v1.y) - Mathf.Atan2(v2norm.x, v2norm.y) + Mathf.PI * 2;
                            }
                            else
                            {
                                angle = Mathf.Atan2(v1.x, v1.y) - Mathf.Atan2(v2norm.x, v2norm.y);
                            }


                            if (angle > Mathf.PI)
                            {
                                compasEnemyDot.transform.position = new Vector3(rotBar.transform.position.x, rotBar.transform.position.y, rotBar.transform.position.z - (Mathf.PI * 2 - angle) / 7.5f);
                            }
                            else
                            {
                                compasEnemyDot.transform.position = new Vector3(rotBar.transform.position.x, rotBar.transform.position.y, rotBar.transform.position.z + angle / 7.5f);

                            }

                            compasEnemyDot.transform.localScale = new Vector3(distanceFormula, distanceFormula, distanceFormula);

                            compasEnemyDot.transform.rotation = rotBar.transform.rotation;

                            dotList.Add(Instantiate(compasEnemyDot));
                        }
                        
                    }
                    else if (hitColliders[i].tag == "Objective")
                    {
                        Vector2 v1 = new Vector2(rotCamOrigin.transform.forward.normalized.x, rotCamOrigin.transform.forward.normalized.z);

                        Vector2 v2 = new Vector2(rotCamOrigin.transform.position.x - hitColliders[i].transform.position.x, rotCamOrigin.transform.position.z - hitColliders[i].transform.position.z);

                        float distanceFormula = 0.3f / (1 + (1 * v2.magnitude / radarSensitivity));

                        //normalize
                        Vector2 v2norm = new Vector2(v2.normalized.x * -1, v2.normalized.y * -1);

                        float camrot = rotCamOrigin.transform.localRotation.eulerAngles.y;

                        float vdot = (v1.x * v2norm.x + v1.y * v2norm.y) * 360;

                        if (camrot > 180)
                        {
                            angle = Mathf.Atan2(v1.x, v1.y) - Mathf.Atan2(v2norm.x, v2norm.y) + Mathf.PI * 2;
                        }
                        else
                        {
                            angle = Mathf.Atan2(v1.x, v1.y) - Mathf.Atan2(v2norm.x, v2norm.y);
                        }


                        if (angle > Mathf.PI)
                        {
                            compasObjectiveDot.transform.position = new Vector3(rotBar.transform.position.x, rotBar.transform.position.y, rotBar.transform.position.z - (Mathf.PI * 2 - angle) / 7.5f);
                        }
                        else
                        {
                            compasObjectiveDot.transform.position = new Vector3(rotBar.transform.position.x, rotBar.transform.position.y, rotBar.transform.position.z + angle / 7.5f);

                        }

                        compasObjectiveDot.transform.localScale = new Vector3(distanceFormula, distanceFormula, distanceFormula);

                        compasObjectiveDot.transform.rotation = rotBar.transform.rotation;

                        dotList.Add(Instantiate(compasObjectiveDot));
                    }
                    else if (hitColliders[i].tag == "Lesser Enemy")
                    {

                        Vector2 v2 = new Vector2(rotCamOrigin.transform.position.x - hitColliders[i].transform.position.x, rotCamOrigin.transform.position.z - hitColliders[i].transform.position.z);

                        if (v2.magnitude < enemyRadarSensitivity)
                        {
                            Vector2 v1 = new Vector2(rotCamOrigin.transform.forward.normalized.x, rotCamOrigin.transform.forward.normalized.z);

                            float distanceFormula = 0.15f / (1 + (1 * v2.magnitude / radarSensitivity));

                            //normalize
                            Vector2 v2norm = new Vector2(v2.normalized.x * -1, v2.normalized.y * -1);

                            float camrot = rotCamOrigin.transform.localRotation.eulerAngles.y;

                            float vdot = (v1.x * v2norm.x + v1.y * v2norm.y) * 360;

                            if (camrot > 180)
                            {
                                angle = Mathf.Atan2(v1.x, v1.y) - Mathf.Atan2(v2norm.x, v2norm.y) + Mathf.PI * 2;
                            }
                            else
                            {
                                angle = Mathf.Atan2(v1.x, v1.y) - Mathf.Atan2(v2norm.x, v2norm.y);
                            }


                            if (angle > Mathf.PI)
                            {
                                compasLesserEnemyDot.transform.position = new Vector3(rotBar.transform.position.x, rotBar.transform.position.y, rotBar.transform.position.z - (Mathf.PI * 2 - angle) / 7.5f);
                            }
                            else
                            {
                                compasLesserEnemyDot.transform.position = new Vector3(rotBar.transform.position.x, rotBar.transform.position.y, rotBar.transform.position.z + angle / 7.5f);

                            }

                            compasLesserEnemyDot.transform.localScale = new Vector3(distanceFormula, distanceFormula, distanceFormula);

                            compasLesserEnemyDot.transform.rotation = rotBar.transform.rotation;

                            dotList.Add(Instantiate(compasLesserEnemyDot));
                        }                       
                    }
                }

                RotSlider(rotCamOrigin.transform.localRotation.eulerAngles.y);
                ShieldSlider(shield);
                ShieldText(shield);
                HPSlider(hp);
                HPText(hp);
                timeleft = updateInterval;
                accum = 0.0F;
                frames = 0;
            }
        }
    }

        void HPText(int Hp)
        {
            if (Hp < 250)
                hpText.color = Color.red;
            else if (Hp < 500)
                hpText.color = Color.yellow;
            else
                hpText.color = Color.green;
        int percHp = Hp / 10;
            hpText.text = "HP " + percHp + "";
        }

        void HPSlider(int Hp)
        {
            if (Hp < 250)
                hpFill.color = Color.red;
            else if (Hp < 500)
                hpFill.color = Color.yellow;
            else
                hpFill.color = Color.green;
        int percHp = Hp / 10;
            hpBar.value = percHp;

        }

        void ShieldText(int Shield)
        {
        int percShield = shield / 10;
            shieldText.text = "" + percShield + "%";
        }

        void ShieldSlider(int Shield)
        {
        int percShield = shield / 10;
            shieldBar.value = percShield;
        }
        void RotSlider(float camRotation)
        {

            float rotationvalue;

            if (camRotation > 180)
            {
                rotationvalue = camRotation - 360;
            }
            else
            {
                rotationvalue = camRotation;
            }

            rotBar.value = rotationvalue;


        }

    public void Damage(int dmg)
    {
        secondCheck = 0;
        if(shield > 0)
        {
            shield -= dmg;
        }
        else
        {
            hp -= dmg;
        }
    }

    } 

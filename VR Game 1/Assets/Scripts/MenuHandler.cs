using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{

    [SerializeField]
    GameObject Screen = null;
    [SerializeField]
    GameObject mainMenu = null;
    [SerializeField]
    GameObject settingsMenu = null;

    public bool InMenu { get; set; }
    // Use this for initialization
    void Start()
    {
        InMenu = true;
        ScreenUp();
        SettingsUp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ScreenUp()
    {
        Screen.transform.Translate(0, 2, 0);
    }

    public void ScreenDown()
    {
        Screen.transform.Translate(0, -2, 0);
    }

    public void MainUp()
    {
        mainMenu.transform.Translate(0, 2, 0);

    }

    public void MainDown()
    {
        mainMenu.transform.Translate(0, -2, 0);

    }

    public void SettingsUp()
    {
        settingsMenu.transform.Translate(0, 2, 0);
    }

    public void SettingsDown()
    {
        settingsMenu.transform.Translate(0, -2, 0);
    }

    public void InMenuChange()
    {
        if (InMenu == true)
        {
            InMenu = false;
        }
        else
        {
            InMenu = true;
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }


}

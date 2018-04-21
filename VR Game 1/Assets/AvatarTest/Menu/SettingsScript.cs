using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {

    [SerializeField]
    GameObject uiToActivate;

    [SerializeField]
    List<Slider> slidersToReset;

    public void ActivateObject()
    {
        if(slidersToReset.Count > 0)
        {
            foreach (Slider slider in slidersToReset)
            {
                slider.value = 0;
            }
        }
        if (uiToActivate != null)
        {
            uiToActivate.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}

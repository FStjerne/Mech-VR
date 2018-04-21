using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasSliderScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField]
    Slider slider;
    bool mouseOver = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if(mouseOver)
        {
            slider.value += 0.01f;
        }
        if(!mouseOver)
        {
            slider.value = 0;
        }
        if(slider.value > 1)
        {
            slider.value = 1;
        }
	}


    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}

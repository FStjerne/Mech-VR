using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

    [SerializeField]
    Slider sensitivitySlider;
    [SerializeField]
    Slider qualitySlider;
	
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/settingsInfo.dat", FileMode.OpenOrCreate);

        SettingsData data = new SettingsData();
        data.MySensitivity = sensitivitySlider.value;
        data.MyQuality = qualitySlider.value;
        bf.Serialize(file, data);
        file.Close();
    }
    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/settingsInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/settingsInfo.dat", FileMode.Open);
            SettingsData data = (SettingsData)bf.Deserialize(file);
            file.Close();

            sensitivitySlider.value = data.MySensitivity;
            qualitySlider.value = data.MyQuality;
        }
    }
}

[Serializable]
class SettingsData
{
    private float sensitivity;

    public float MySensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }

    private float quality;

    public float MyQuality
    {
        get { return quality; }
        set { quality = value; }
    }
}

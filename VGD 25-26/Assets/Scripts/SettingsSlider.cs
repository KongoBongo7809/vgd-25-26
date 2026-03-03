using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSlider : MonoBehaviour
{
    public Slider slider;
    public string soundTag;

    private List<Sound> soundsWithTag;

    private void Start()
    {
        Debug.Log("start");
        soundsWithTag = new List<Sound>();
        Sound[] allSounds = FindAnyObjectByType<AudioManager>().sounds;
        foreach(Sound s in allSounds)
        {
            if(s.tag == soundTag) soundsWithTag.Add(s);
        }
    }

    private void Update()
    {
        foreach(Sound s in soundsWithTag)
        {
            s.volume *= slider.value;
        }
    }
}

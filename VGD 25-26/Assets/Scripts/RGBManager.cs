using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RGBManager : MonoBehaviour
{
    private Volume vol;
    private ColorAdjustments ca;
    public int[] scenesWithRGB;
    private bool hasRGB = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vol = FindFirstObjectOfType<Volume>();
        vol.profile.TryGet(out ca);

        for(int i = 0; i < scenesWithRGB.length; i++)
        {
            if(i == (SceneManager.GetActiveScene().buildIndex + 1))
            {
                hasRGB = true;
            }
        }
    }

    void CheckForRGB()
    {
        /*if(hasRGB)
        {
            ca.intensity.value = 1f;
        }
        else
        {
            ca.intensity.value = 0f;
        }*/
    }
}

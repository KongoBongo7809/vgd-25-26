using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class RGBManager : MonoBehaviour
{
    private Volume vol;
    private ColorAdjustments ca;
    public int[] scenesWithRGB;
    private bool hasRGB = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vol = FindFirstObjectByType<Volume>();
        vol.profile.TryGet(out ca);

        for(int i = 0; i < scenesWithRGB.Length; i++)
        {
            if(scenesWithRGB[i] == (SceneManager.GetActiveScene().buildIndex))
            {
                hasRGB = true;
            }
        }

        CheckForRGB();
    }

    public void CheckForRGB()
    {
        if(hasRGB)
        {
            ca.active = true;
        }
        else
        {
            ca.active = false;
        }
    }
}

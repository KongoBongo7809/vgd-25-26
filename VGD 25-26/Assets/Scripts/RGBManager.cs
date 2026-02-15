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
    //public int[] scenesWithRGB;
    public bool hasRGB = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vol = FindFirstObjectByType<Volume>();
        vol.profile.TryGet(out ca);
        CheckSceneForRGB();
    }

    public void CheckSceneForRGB()
    {
        //if (targetIndex == null) targetIndex = 0;
        /*for(int i = 0; i < scenesWithRGB.Length; i++)
        {
            Debug.Log("index: " + i + ", value: " + scenesWithRGB[i] + ", scene: " + SceneManager.GetActiveScene().buildIndex + ", target: " + targetIndex);

            if(scenesWithRGB[i] == targetIndex)
            {
                hasRGB = true;
            }
        }*/

        if(hasRGB)
        {
            ca.active = true;
            Debug.Log("activated");
        }
        else
        {
            ca.active = false;
            Debug.Log("deactivated");
        }
    }
}

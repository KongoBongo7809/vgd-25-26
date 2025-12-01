using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HueChange : MonoBehaviour
{
    [SerializeField] Volume vol;
    private ChannelMixer cm;
    private bool isRed = false;
    private bool isGreen = false;
    private bool isBlue = false;

    void Start()
    {
        vol.profile.TryGet(out cm);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            changeRed();
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            changeGreen();
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            changeBlue();
        }
    }

    public void changeRed()
    {
        if(!isRed)
        {
            Debug.Log("red ON");
            cm.redOutRedIn.value = 90f;
            isRed = true;
        }
        else
        {
            Debug.Log("red OFF");
            cm.redOutRedIn.value = 10f;
            isRed = false;
        }
    }

    public void changeGreen()
    {
        if(!isGreen)
        {
            Debug.Log("green ON");
            cm.greenOutRedIn.value = 90f;
            isGreen = true;
        }
        else
        {
            Debug.Log("green OFF");
            cm.greenOutRedIn.value = 10f;
            isGreen = false;
        }
    }

    public void changeBlue()
    {
        if(!isBlue)
        {
            Debug.Log("blue ON");
            cm.blueOutRedIn.value = 90f;
            isBlue = true;
        }
        else
        {
            Debug.Log("blue OFF");
            cm.blueOutRedIn.value = 10f;
            isBlue = false;
        }
    }
}

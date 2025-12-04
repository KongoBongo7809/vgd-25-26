using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorChange : MonoBehaviour
{
    [SerializeField] LayerMask redLayer;
    [SerializeField] LayerMask greenLayer;
    [SerializeField] LayerMask blueLayer;

    public Volume vol;
    private ColorAdjustments ca;
    private bool isRed = true;
    private bool isGreen = false;
    private bool isBlue = false;

    private Color currentColor;
    private float elapsed;
    public float speed;

    private Color RED;
    private Color GREEN;
    private Color BLUE;

    void Start()
    {
        //Define constants
        RED = new Color(255f/255f, 128f/255f, 128f/255f, 1f);
        GREEN = new Color(128f/255f, 255f/255f, 128f/255f, 1f);
        BLUE = new Color(128f/255f, 128f/255f, 255f/255f, 1f);

        vol.profile.TryGet(out ca);
        currentColor = ca.colorFilter.value;
    }

    void Update()
    {
        elapsed += Time.deltaTime / speed;

        if(Input.GetKeyDown(KeyCode.R))
        {
            if(!isRed)
            {
                elapsed = 0;
                Debug.Log("red");
                Color newColor = Color.Lerp(currentColor, RED, elapsed);
                ca.colorFilter.value = newColor;
                //ca.colorFilter.value = Color.Lerp(currentColor, new Color(255f/255f, 128f/255f, 128f/255f, 1f), elapsed);
                //Color newColor = Color.Lerp(currentColor, new Color(255f/255f, 128f/255f, 128f/255f, 1f), elapsed);
                if(elapsed >= 1f) { return; }
                //ca.colorFilter.value = newColor;

                
            }

            isRed = true;
            isGreen = false;
            isBlue = false;
            currentColor = RED;
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            if(!isGreen)
            {
                elapsed = 0;
                Debug.Log("green");
                Color newColor = Color.Lerp(currentColor, GREEN, elapsed);
                ca.colorFilter.value = newColor;
                //Color newColor = Color.Lerp(currentColor, new Color(128f/255f, 255f/255f, 128f/255f, 1f), elapsed);
                if(elapsed >= 1f) { return; }
                //ca.colorFilter.value = newColor;                
            }

            isRed = false;
            isGreen = true;
            isBlue = false;
            currentColor = GREEN;
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            if(!isBlue)
            {
                elapsed = 0;
                Debug.Log("blue");
                Color newColor = Color.Lerp(currentColor, BLUE, elapsed);
                ca.colorFilter.value = newColor;
                //Color newColor = Color.Lerp(currentColor, new Color(128f/255f, 128f/255f, 255f/255f, 1f), elapsed);
                if(elapsed >= 1f) { return; }
                //ca.colorFilter.value = newColor;

                
            }

            isRed = false;
            isGreen = false;
            isBlue = true;
            currentColor = BLUE; 
        }
    }

    /*public void changeRed()
    {
        
    }

    public void changeGreen()
    {
        
    }

    public void changeBlue()
    {
        
    }*/
}

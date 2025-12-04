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
    private bool isRed = false;
    private bool isGreen = false;
    private bool isBlue = false;

    private Color currentColor;
    private float elasped;
    private float elapsed;
    public float speed;

    void Start()
    {

        vol.profile.TryGet(out ca);
        currentColor = ca.colorFilter.value;
    }

    void Update()
    {
        speed += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.R))
        {
            if(!isRed)
            {
                elapsed += Time.deltaTime / speed;
                Debug.Log("red");
                Color newColor = Color.Lerp(currentColor, new Color(255f/255f, 128f/255f, 128f/255f, 1f), elapsed);
                if(elapsed >= 1f) { elapsed = 0f; }
                ca.colorFilter.value = newColor;
                currentColor = new Color(255f/255f, 128f/255f, 128f/255f, 1f);

                isRed = true;
                isGreen = false;
                isBlue = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            if(!isGreen)
            {
                elapsed += Time.deltaTime / speed;
                Debug.Log("green");
                Color newColor = Color.Lerp(currentColor, new Color(128f/255f, 255f/255f, 128f/255f, 1f), speed);
                if(elapsed >= 1f) { elapsed = 0f; }
                ca.colorFilter.value = newColor;
                currentColor = new Color(128f/255f, 255f/255f, 128f/255f, 1f);

                isRed = false;
                isGreen = true;
                isBlue = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            if(!isBlue)
            {
                elapsed += Time.deltaTime / speed;
                Debug.Log("blue");
                Color newColor = Color.Lerp(currentColor, new Color(128f/255f, 128f/255f, 255f/255f, 1f), speed);
                if(elapsed >= 1f) { elapsed = 0f; }
                ca.colorFilter.value = newColor;
                currentColor = new Color(128f/255f, 128f/255f, 255f/255f, 1f);

                isRed = false;
                isGreen = false;
                isBlue = true;
            }
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

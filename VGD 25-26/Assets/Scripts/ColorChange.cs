using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorChange : MonoBehaviour
{
    public string redLayer;
    public string greenLayer;
    public string blueLayer;

    public Volume vol;
    private ColorAdjustments ca;
    private bool isRed = true;
    private bool isGreen = false;
    private bool isBlue = false;

    private Color currentColor;
    private Color targetColor;
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
        targetColor = currentColor;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isRed)
        {
            currentColor = ca.colorFilter.value;
            targetColor = RED;
            elapsed = 0f;

            isRed = true;
            isGreen = false;
            isBlue = false;

            ChangeActive(redLayer, true);
            ChangeActive(greenLayer, false);
            ChangeActive(blueLayer, false);
        }
        if (Input.GetKeyDown(KeyCode.G) && !isGreen)
        {
            currentColor = ca.colorFilter.value;
            targetColor = GREEN;
            elapsed = 0f;

            isGreen = true;
            isRed = false;
            isBlue = false;

            ChangeActive(redLayer, false);
            ChangeActive(greenLayer, true);
            ChangeActive(blueLayer, false);
        }
        if (Input.GetKeyDown(KeyCode.B) && !isBlue)
        {
            currentColor = ca.colorFilter.value;
            targetColor = BLUE;
            elapsed = 0f;

            isBlue = true;
            isRed = false;
            isGreen = false;

            ChangeActive(redLayer, false);
            ChangeActive(greenLayer, false);
            ChangeActive(blueLayer, true);
        }

        elapsed += Time.deltaTime / speed;
        ca.colorFilter.value = Color.Lerp(currentColor, targetColor, elapsed);
    }

    void ChangeActive(string tag, bool on)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if(on)
            {
                gameObjects[i].GetComponent<SpriteRenderer>().color.a = (255f/255f);
                gameObjects[i].GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                gameObjects[i].GetComponent<SpriteRenderer>().color.a = (50f/255f);
                gameObjects[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}

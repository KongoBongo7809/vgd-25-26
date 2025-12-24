using UnityEngine;

public class Fullscreen : MonoBehaviour
{
    private float orthoSizeDefault = 5f;

    void Awake()
    {
        float newScale = Camera.main.orthographicSize / orthoSizeDefault;
        transform.localScale *= newScale;
    }
}

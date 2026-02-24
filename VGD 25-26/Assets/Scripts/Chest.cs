using UnityEngine;

public class Chest : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        FindFirstObjectByType<SceneManagement>().LoadMap();
    }
}

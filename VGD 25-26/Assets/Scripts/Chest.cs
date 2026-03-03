using UnityEngine;

public class Chest : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player") FindFirstObjectByType<SceneManagement>().LoadMap();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MainMenu : MonoBehaviour
{
    public SceneManagement sceneManagement;

    public void StartGame()
    {
        sceneManagement.LoadNextLevel();
    }
}

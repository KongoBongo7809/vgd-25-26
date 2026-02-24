using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MainMenu : MonoBehaviour
{
    public SceneManagement sceneManagement;

    public void StartGame()
    {
        PlayerPrefs.SetInt("levelsUnlocked", 0);
        Debug.Log("levels unlocked set to 0");
        sceneManagement.LoadNextLevel();
    }
}

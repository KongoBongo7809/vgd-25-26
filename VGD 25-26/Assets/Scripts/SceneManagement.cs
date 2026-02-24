using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public Animator transition;
    private RGBManager rgb;

    public float transitionTime = 1f;
    public int cutsceneIndex = 5;

    public void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        rgb = FindFirstObjectByType<RGBManager>();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        //rgb.CheckSceneForRGB(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevelWithIndex(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    public void LoadCutscene()
    {
        StartCoroutine(LoadLevel(cutsceneIndex));
    }

    public void LoadMap()
    {
        FindFirstObjectByType<AudioManager>().Play("Lock");
        StartCoroutine(LoadLevel(2));
    }

    IEnumerator LoadLevel(int index)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
    }
}
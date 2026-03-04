using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SenseiCutscenes : MonoBehaviour
{
    public string[] sentences;
    public GameObject cutsceneOverlay;
    public TextMeshProUGUI cutsceneText;
    public SmoothCamera smoothCamera;
    public Transform newCameraTarget;
    public Vector3 newCameraOffset;
    public Animator animator;

    //Camera
    private Transform originalPosition;
    private Vector3 originalOffset;

    private int index = 0;

    private void Start()
    {
        originalPosition = smoothCamera.target;
        originalOffset = smoothCamera.offset;
        TriggerCutscene();
    }

    public void TriggerCutscene()
    {
        animator.SetTrigger("Next Scene");
        smoothCamera.SetTarget(newCameraTarget);
        smoothCamera.SetOffset(newCameraOffset);
        cutsceneOverlay.SetActive(true);
        cutsceneText.text = sentences[index];
        index++;
    }

    public void ResumeGame()
    {
        Debug.Log("Resume Game");
        smoothCamera.SetTarget(originalPosition);
        smoothCamera.SetOffset(originalOffset);
        cutsceneOverlay.SetActive(false);
    }
}

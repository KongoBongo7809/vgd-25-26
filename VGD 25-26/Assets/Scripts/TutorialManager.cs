using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public string[] sentences;
    public GameObject tutorialOverlay;
    public TextMeshProUGUI tutorialText;

    private int index = 0;
    private GameObject currentTrigger;

    public void TriggerTutorial()
    {
        tutorialOverlay.SetActive(true);
        tutorialText.text = sentences[index];
        index++;
    }

    public void ResumeGame()
    {
        tutorialOverlay.SetActive(false);
        Time.timeScale = 1f;
        Destroy(currentTrigger);
    }

    public void SetTutorialTrigger(GameObject trigger)
    {
        currentTrigger = trigger;
    }
}

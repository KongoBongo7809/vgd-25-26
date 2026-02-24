using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public DialogueTrigger trigger;

    void Start()
    {
        AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        audioManager.Stop("Home Music");
        audioManager.Play("Level Music");
        trigger.TriggerDialogue();
    }
}

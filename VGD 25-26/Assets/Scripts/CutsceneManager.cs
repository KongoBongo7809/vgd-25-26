using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public DialogueTrigger trigger;

    void Start()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.Stop("Home Music");
        audioManager.Play("Level Music");
        trigger.TriggerDialogue();
    }
}

using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public DialogueTrigger trigger;

    void Start()
    {
        trigger.TriggerDialogue();
    }
}

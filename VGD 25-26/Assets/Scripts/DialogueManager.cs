using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public TextMeshProUGUI dialogueText;
    [SerializeField] private float typingSpeed = 0.05f;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        Debug.Log(sentences.Count);

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        AudioManager audio = FindAnyObjectByType<AudioManager>();
        audio.Play("Text");
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            float delay = ".,:;?!".IndexOf(letter) >= 0 
                          ? typingSpeed * 8f 
                          : typingSpeed;
            yield return new WaitForSeconds(delay);
        }
        audio.Stop("Text");
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        FindAnyObjectByType<SceneManagement>().LoadNextLevel();
    }
}

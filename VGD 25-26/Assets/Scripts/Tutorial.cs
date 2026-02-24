using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public TutorialManager tManager;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            //Trigger tutorial dialogue
            tManager.TriggerTutorial();

            //Pause game
            Time.timeScale = 0f;

            tManager.SetTutorialTrigger(gameObject);
        }
    }
}

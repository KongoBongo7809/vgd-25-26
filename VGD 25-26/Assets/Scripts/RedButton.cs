using UnityEngine;

public class RedButton : MonoBehaviour
{
    private bool pressed = false;
    [SerializeField] private Animator animator;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            pressed = true;
            animator.SetBool("isPressed", true);
            FindFirstObjectByType<AudioManager>().Play("Enemy Attack");
        }
    }

    public bool isPressed()
    {
        return pressed;
    }
}

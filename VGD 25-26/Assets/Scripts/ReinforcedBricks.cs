using UnityEngine;

public class ReinforcedBricks : MonoBehaviour
{
    public RedButton button;

    void Update()
    {
        if(!button.isPressed())
        {
            Collider2D collider = gameObject.GetComponent<Collider2D>();
            if (collider != null) collider.enabled = true;
        }
    }
}

using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            PlayerCombat player = collider.gameObject.GetComponent<PlayerCombat>();
            if (player != null) player.Die();
        }
    }
}

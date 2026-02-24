using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    public GameObject portal;
    public float delayTime = 1f;
    public ParticleSystem teleportEffect;
    public float cooldownTime = 1f;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(delayTime);
        Collider2D targetPortalCollider = portal.GetComponent<Collider2D>();
        if (targetPortalCollider != null)
        {
            targetPortalCollider.enabled = false;
            PlayerManager.instance.player.transform.position = new Vector2(portal.transform.position.x, portal.transform.position.y);
            ParticleSystem currentTeleportEffect = Instantiate(teleportEffect, PlayerManager.instance.player.transform.position, PlayerManager.instance.player.transform.rotation);
            currentTeleportEffect.Play();
            yield return new WaitForSeconds(cooldownTime);
            targetPortalCollider.enabled = true;
        }
    }
}

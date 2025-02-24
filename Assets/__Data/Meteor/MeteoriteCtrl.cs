using __Data;
using UnityEngine;

public class MeteoriteCtrl : GameBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ground")  || collider.gameObject.CompareTag("Player"))
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                PlayerController playerController = collider.transform.GetComponent<PlayerController>();
            
                playerController.TakeDamage();
            }
            
            Destroy(gameObject);
        }
    }
}

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
            CreateImpactFX();
            Destroy(gameObject);
        }
    }
    
    protected virtual void CreateImpactFX()
    {
        string fxName = GetImpactFX();

        Vector3 hitPos = transform.position;
        Transform fxImpact = FXSpawner.Instance.Spawn(fxName, hitPos, Quaternion.identity);
        fxImpact.gameObject.SetActive(true);
    }

    protected virtual string GetImpactFX()
    {
        return FXSpawner.meteorFire_1;
    }
}

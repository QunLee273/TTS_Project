using __Data.Script;
using UnityEngine;

public class PlayerController : ObjController
{
    [SerializeField] protected Transform respawnPoint;
    private bool _isAlive = true; 

    public bool IsAlive
    {
        get => _isAlive;
        private set
        {
            _isAlive = value;
            ObjMovement.Animator.SetBool(AnimString.isAlive, _isAlive);
            ObjMovement.Animator.SetBool(AnimString.canMove, _isAlive);
        }
    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.CompareTag("Trap") || collide.CompareTag("Holder"))
        {
            Debug.Log($"Player hit: {collide.gameObject.name}");
            HandleDeath();
        }
        
        if (collide.CompareTag("Checkpoint"))
        {
            Debug.Log($"Checkpoint reached: {collide.gameObject.name}");
            respawnPoint.position = collide.transform.position;
        }
    }

    private void HandleDeath()
    {
        if (IsAlive)
        {
            IsAlive = false;
            ObjMovement.Animator.Play("Player_Death");
            Invoke(nameof(Respawn), 2f);
        }
    }

    private void Respawn()
    {
        Debug.Log("Respawning...");
        IsAlive = true;
        transform.position = respawnPoint.position; 
        ObjMovement.Animator.Play("Player_Idle"); 
    }
}
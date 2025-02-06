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
            Death();
        }
        
        if (collide.CompareTag("Checkpoint"))
        {
            Debug.Log($"Checkpoint reached: {collide.gameObject.name}");
            respawnPoint.position = collide.transform.position;
        }
    }

    public void Death()
    {
        if (!IsAlive) return;
        
        DamageReceiver.Deduct(1);
        IsAlive = false;
        ObjMovement.Animator.Play("Player_Death"); 
        
        if (DamageReceiver.Lifes > 0)
            Invoke(nameof(Respawn), 2f);
    }

    public void Respawn()
    {
        Debug.Log("Respawning...");
        IsAlive = true;
        transform.position = respawnPoint.position; 
        ObjMovement.Animator.Play("Player_Idle"); 
    }

    protected override string GetObjectTypeString()
    {
        return ObjectType.Player.ToString();
    }
}
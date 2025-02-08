using __Data.Script;
using UnityEngine;
using System.Collections;

public class PlayerController : ObjController
{
    [Header("Player Controller")]
    [SerializeField] protected Transform respawnPoint;
    [SerializeField] protected bool isAlive = true; 
    public bool IsAlive
    {
        get => isAlive;
        private set
        {
            isAlive = value;
            ObjMovement.Animator.SetBool(AnimString.isAlive, isAlive);
            ObjMovement.Animator.SetBool(AnimString.canMove, isAlive);
        }
    }
    
    [SerializeField] private float damageCooldown = 1f; 
    private bool _isInvulnerable = false;
    
    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.CompareTag("Trap") || collide.CompareTag("Holder"))
        {
            Debug.Log($"Player hit: {collide.gameObject.name}");
            TakeDamage(1);
        }
        
        if (collide.CompareTag("Checkpoint"))
        {
            Debug.Log($"Checkpoint reached: {collide.gameObject.name}");
            respawnPoint.position = collide.transform.position;
        }
    }
    
    public void TakeDamage(int damage)
    {
        if (_isInvulnerable) return;
        
        DamageReceiver.Deduct(damage);
        
        IsAlive = false;
    
        if (DamageReceiver.Lifes > 0)
            Invoke(nameof(Respawn), 2f);
        
        StartCoroutine(DamageCooldownCoroutine());
    }
    
    private IEnumerator DamageCooldownCoroutine()
    {
        _isInvulnerable = true;
        yield return new WaitForSeconds(damageCooldown);
        _isInvulnerable = false;
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
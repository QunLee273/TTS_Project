using __Data.Script;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class PlayerController : ObjController
{
    [Header("Player Controller")]
    [SerializeField] protected Transform respawnPoint;
    [SerializeField] protected bool isAlive = true; 
    public bool IsAlive
    {
        get => isAlive;
        set
        {
            isAlive = value;
            ObjMovement.Animator.SetBool(AnimString.isAlive, isAlive);
            ObjMovement.Animator.SetBool(AnimString.canMove, isAlive);
        }
    }
    
    [SerializeField] private float damageCooldown = 1f; 
    public bool isInvulnerable = false;
    
    protected override bool IsDebugEnabled => true;

    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.CompareTag("Trap") || collide.CompareTag("Holder"))
        {
            Debug.Log($"Player hit: {collide.gameObject.name}");
            TakeDamage();
        }
        
        if (collide.CompareTag("Checkpoint"))
        {
            Debug.Log($"Checkpoint reached: {collide.gameObject.name}");
            respawnPoint.position = collide.transform.position;
        }
    }
    
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage();
        }
    }
    
    public void TakeDamage()
    {
        if (isInvulnerable) return;
        
        DamageReceiver.Deduct(1);
        
        IsAlive = false;
    
        if (DamageReceiver.Lifes > 0)
            Invoke(nameof(Respawn), 2f);
        
        StartCoroutine(DamageCooldownCoroutine());
    }

    public IEnumerator DamageCooldownCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(damageCooldown);
        isInvulnerable = false;
    }

    public void Respawn()
    {
        IsAlive = true;
        transform.position = respawnPoint.position; 
        ObjMovement.Animator.Play("Player_Idle"); 
    }

    protected override string GetObjectTypeString()
    {
        return ObjectType.Player.ToString();
    }
}
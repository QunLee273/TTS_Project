using __Data.Script;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using UnityEngine.tvOS;

public class PlayerController : ObjController
{
    [Header("Player Controller")]
    private static PlayerController instance;
    public static PlayerController Instance => instance;
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
    
    [SerializeField] protected float damageCooldown = 1f; 
    [SerializeField] protected bool isInvulnerable = false;

    public bool IsInvulnerable
    {
        get => isInvulnerable;
        set => isInvulnerable = value;
    }
    
    protected override bool IsDebugEnabled => true;

    protected override void Awake()
    {
        base.Awake();
        if (PlayerController.instance != null) Debug.LogError("Only 1 PlayerController allow to exist");
        PlayerController.instance = this;
    }

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
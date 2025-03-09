using __Data.Script;
using UnityEngine;
using System.Collections;

public class PlayerController : ObjController
{
    [Header("Player Controller")]
    private static PlayerController _instance;
    public static PlayerController Instance => _instance;
    [SerializeField] protected Transform respawnPoint;
    [SerializeField] protected bool isAlive = true; 
    public bool IsAlive
    {
        get => isAlive;
        set
        {
            isAlive = value;
            ObjMovement.Animator.SetBool(AnimString.isAlive, isAlive);
        }
    }
    
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
        if (PlayerController._instance != null) Debug.LogError("Only 1 PlayerController allow to exist");
        PlayerController._instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.CompareTag("Checkpoint"))
        {
            //Debug.Log($"Checkpoint reached: {collide.gameObject.name}");
            respawnPoint.position = collide.transform.position;
        }
    }
    
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage();
        }
        
        if (collision.gameObject.CompareTag("Holder"))
        {
            AudioManager.Instance.PlaySfx("Dead2");
            DamageReceiver.Deduct(1);
            IsAlive = false;
        }
    }
    
    public void TakeDamage()
    {
        if (isInvulnerable) return;
        
        AudioManager.Instance.PlaySfx("Dead1");
        DamageReceiver.Deduct(1);
        isInvulnerable = true;
        IsAlive = false;
    }

    public void Respawn()
    {
        if (DamageReceiver.Lifes < 0) return;
        IsAlive = true;
        transform.position = respawnPoint.position; 
        ObjMovement.Animator.Play("Player_Idle");
        isInvulnerable = false;
    }

    protected override string GetObjectTypeString()
    {
        return ObjectType.Player.ToString();
    }
}
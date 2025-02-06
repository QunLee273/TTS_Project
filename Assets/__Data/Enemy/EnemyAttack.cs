using System.Collections.Generic;
using __Data.Script;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAttack : AbilityAttack
{
    [Header("EnemyAttack")]
    [SerializeField] protected Collider2D col;
    
    [SerializeField] protected List<Collider2D> detectedAttack = new List<Collider2D>();
    
    [SerializeField] protected bool hasTarget;
    [SerializeField] protected bool isAttacking = false;
    
    [SerializeField] protected Collider2D currentTarget = null;

    private bool HasTarget
    {
        get => hasTarget;
        set
        {
            hasTarget = value;
            animator.SetBool(AnimString.hasTarget, value);
        }
    }
    
    [SerializeField] protected bool canMove;

    public bool CanMove
    {
        get => canMove;
        private set
        {
            canMove = value;
            animator.SetBool(AnimString.canMove, value);
        }
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        
        LoadCollider2D();
    }

    protected void FixedUpdate()
    {
        if (!isAttacking)
        {
            HasTarget = detectedAttack.Count > 0;
            CanMove = !HasTarget;

            if (HasTarget)
            {
                if (!currentTarget && detectedAttack.Count > 0)
                {
                    currentTarget = detectedAttack[0];
                }
                animator.SetTrigger(AnimString.attackTrigger);
                isAttacking = true;
            }
        }
    }

    private void LoadCollider2D()
    {
        if (col != null) return;
        col = GetComponent<Collider2D>();
        Debug.LogWarning(transform.name + ": LoadCollider2D", gameObject);
    }
    
    public void DealDamage()
    {
        foreach (Collider2D player in detectedAttack.ToArray())
        {
            PlayerController playerController = currentTarget.GetComponentInChildren<PlayerController>();
            
            playerController.Death();
            col.enabled = false;
        }
        
        col.enabled = true;
        currentTarget = null;
        isAttacking = false;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        detectedAttack.Add(other);
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        detectedAttack.Remove(other);
    }
}

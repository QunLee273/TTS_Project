using System.Collections.Generic;
using __Data.Script;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAttack : AbilityAttack
{
    [Header("EnemyAttack")]
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
    
    public void EnemySenderDamage()
    {
        foreach (Collider2D player in detectedAttack.ToArray())
        {
            PlayerController playerController = currentTarget.GetComponentInChildren<PlayerController>();
            
            playerController.TakeDamage(1);
            Debug.Log(player.gameObject.name + playerController.DamageReceiver.Lifes);
        }
        
        currentTarget = null;
        isAttacking = false;
    }
}

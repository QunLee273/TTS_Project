using System.Collections.Generic;
using __Data.Script;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAttack : AbilityAttack
{
    [Header("Enemy Attack")]
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
        if (isAttacking) return;
        HasTarget = detectedAttack.Count > 0;
        CanMove = !HasTarget;

        if (!HasTarget) return;
        if (!currentTarget && detectedAttack.Count > 0)
        {
            currentTarget = detectedAttack[0];
        }
        animator.SetTrigger(AnimString.attackTrigger);
        isAttacking = true;
    }
    
    public void EnemyMeleeSenderDam()
    {
        AudioManager.Instance.PlaySfx("Melee");
        foreach (Collider2D player in detectedAttack.ToArray())
        {
            PlayerController playerController = currentTarget.GetComponentInChildren<PlayerController>();
            
            playerController.TakeDamage();
            Debug.Log(player.gameObject.name + playerController.DamageReceiver.Lifes);
        }
        
        currentTarget = null;
        isAttacking = false;
    }
    
    public void EnemyRangedSenderDam()
    {
        AudioManager.Instance.PlaySfx("Ranged");
        Vector3 spawnPos = transform.position;

        Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.bullet2, spawnPos, Quaternion.identity);
        if (newBullet == null) return;

        newBullet.gameObject.SetActive(true);
        BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
        ObjFly objFly = newBullet.GetComponentInChildren<BulletFly>();
        if (bulletCtrl == null) return;
        bulletCtrl.SetShooter(transform.parent.parent);
        
        float shooterDirection = bulletCtrl.Shooter.localScale.x;
        objFly.direction = (shooterDirection >= 0) ? Vector3.right : Vector3.left;
        
        currentTarget = null;
        isAttacking = false;
    }
}

using __Data.Script;
using UnityEngine;

public class PlayerAttack : AbilityAttack
{
    [Header("Player Attack")]
    [SerializeField] protected float attackCooldown = 0.5f;
    [SerializeField] protected float attackTimer = 0f;
        
    [SerializeField] protected bool isAttacking = false;
    
    protected void Update()
    {
        if (!animator.GetBool(AnimString.isAlive)) return;

        if (attackTimer < attackCooldown) attackTimer += Time.deltaTime;
        
        if (!isAttacking && attackTimer >= attackCooldown)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Attack();
            }
        }
    }
    
    private void Attack()
    {
        isAttacking = true;
        attackTimer = 0f;

        if (detectedAttack.Count > 0)
            MeleeAttack();
        else
            RangedAttack();
    }
    
    private void MeleeAttack()
    {
        animator.SetTrigger(AnimString.attackTrigger);
        animator.Play("Player_Melee");
    }
    
    private void RangedAttack()
    {
        animator.SetTrigger(AnimString.attackTrigger);
        animator.Play("Player_Ranged");
    }
    
    public void OnClick()
    {
        if (animator.GetBool(AnimString.isAlive) && !isAttacking && attackTimer >= attackCooldown)
        {
            Attack();
        }
    }
    
    public void StartMelee()
    {
        foreach (Collider2D enemy in detectedAttack.ToArray())
        {
            EnemyCtrl enemyCtrl = enemy.GetComponentInChildren<EnemyCtrl>();
            if (enemyCtrl != null)
                enemyCtrl.DamageReceiver.Deduct(1);
        }
    }
    
    public void StartRanged()
    {
        Vector3 spawnPos = transform.position;
        Quaternion spawnRot = transform.parent.rotation;

        Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.bullet1, spawnPos, spawnRot);
        if (newBullet == null) return;

        newBullet.gameObject.SetActive(true);
        BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
        if (bulletCtrl != null)
            bulletCtrl.SetShooter(transform.parent);
    }

    public void EndAttacking()
    {
        isAttacking = false;
    }
}

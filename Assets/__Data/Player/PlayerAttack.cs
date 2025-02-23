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
            isAttacking = false;
        }
    }
    
    public void StartMelee()
    {
        foreach (Collider2D enemy in detectedAttack.ToArray())
        {
            EnemyCtrl enemyCtrl = enemy.GetComponentInChildren<EnemyCtrl>();
            if (enemyCtrl != null)
                enemyCtrl.DamageReceiver.Deduct(enemyCtrl.DamageReceiver.Lifes);
            
            BreakablesCtrl breakablesCtrl = enemy.GetComponentInChildren<BreakablesCtrl>();
            if (breakablesCtrl != null)
                breakablesCtrl.DamageReceiver.Deduct(breakablesCtrl.DamageReceiver.Lifes);
            
            BossCtrl bossCtrl = enemy.GetComponentInChildren<BossCtrl>();
            if (bossCtrl != null)
                bossCtrl.DamageReceiver.Deduct(5);
        }
    }
    
    public void StartRanged()
    {
        Vector3 spawnPos = transform.position;

        Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.bullet1, spawnPos, Quaternion.identity);
        if (newBullet == null) return;

        newBullet.gameObject.SetActive(true);
        BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
        ObjFly objFly = newBullet.GetComponentInChildren<BulletFly>();
        if (bulletCtrl != null)
        {
            bulletCtrl.SetShooter(transform.parent.parent);
            Debug.Log(bulletCtrl.Shooter.name);
            
            float shooterDirection = bulletCtrl.Shooter.localScale.x;
            objFly.direction = (shooterDirection >= 0) ? Vector3.right : Vector3.left;
        }
            
    }
}

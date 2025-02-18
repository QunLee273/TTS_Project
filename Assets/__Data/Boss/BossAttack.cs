using __Data.Script;
using UnityEngine;

public class BossAttack : AbilityAttack
{
    [Header("Boss Attack")]
    [SerializeField] protected float attackMelee = 2f;
    [SerializeField] protected float attackRanged = 5f;
    [SerializeField] protected float attackDelay = 1f;
    [SerializeField] protected float attackTimer = 0f;
    
    [SerializeField] protected Transform target;
    [SerializeField] protected Transform spawnPoint;
    [SerializeField] protected bool isAttacking = false;

    public bool IsLive => animator.GetBool(AnimString.isAlive);

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTarget();
    }

    private void LoadTarget()
    {
        if (target != null) return;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.LogWarning(transform.name + ": LoadTarget", gameObject);
    }

    protected void Update()
    {
        if (!IsLive) return;
        if (attackTimer < attackDelay) attackTimer += Time.deltaTime;
        if (!isAttacking && attackTimer >= attackDelay)
        {
            Attack();
            isAttacking = false;
        }
    }

    private void Attack()
    {
        isAttacking = true;
        attackTimer = 0f;

        float direction = Vector3.Distance(transform.position, target.position);

        if (direction <= attackMelee)
        {
            animator.SetTrigger(AnimString.atkMeleeTrigger);
            animator.Play("Boss_Melee");
        }
        else if (direction <= attackRanged)
        {
            animator.SetTrigger(AnimString.atkRangedTrigger);
            animator.Play("Boss_Ranged");
        }
        
    }
    
    public void BossMeleeAttack()
    {
        foreach (Collider2D player in detectedAttack.ToArray())
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
                playerController.TakeDamage();
        }
    }

    public void BossRangedAttack()
    {
        Vector3 spawnPos = spawnPoint.position;

        Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.bullet3, spawnPos, Quaternion.identity);
        if (newBullet == null) return;

        newBullet.gameObject.SetActive(true);
        BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
        ObjFly objFly = newBullet.GetComponentInChildren<BulletFly>();
        
        if (bulletCtrl == null) return;
        bulletCtrl.SetShooter(transform.parent);
        
        float shooterDirection = bulletCtrl.Shooter.parent.localScale.x;
        objFly.direction = (shooterDirection >= 0) ? Vector3.right : Vector3.left;
    }
}

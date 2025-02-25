using System;
using __Data.Script;
using UnityEngine;

public class BossAttack : AbilityAttack
{
    [Header("Boss Attack")]
    [SerializeField] protected float attackMelee = 2f;
    [SerializeField] protected float attackRanged = 9f;
    [SerializeField] protected int countRanged = 5;
    [SerializeField] protected float recoveryBullet = 7.5f;
    [SerializeField] protected float attackDelay = 2.5f;
    [SerializeField] protected float attackTimer = 0f;
    
    [SerializeField] protected Transform target;
    [SerializeField] protected Transform spawnPoint;
    
    [SerializeField] protected bool isAttacking = false;
    private float _timer;
    private int _count;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTarget();
        _count = countRanged;
    }

    private void LoadTarget()
    {
        if (target != null) return;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.LogWarning(transform.name + ": LoadTarget", gameObject);
    }

    protected void FixedUpdate()
    {
        if (_count < countRanged) _timer += Time.deltaTime;
        if (_timer >= recoveryBullet)
        {
            _count++;
            _timer = 0;
        }
    }

    protected void Update()
    {
        if (!animator.GetBool(AnimString.isAlive)) return;
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
        else if (direction <= attackRanged && _count > 0)
        {
            animator.SetTrigger(AnimString.atkRangedTrigger);
            animator.Play("Boss_Ranged");
            _count--;
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
        bulletCtrl.SetShooter(transform.parent.parent);
        
        float shooterDirection = bulletCtrl.Shooter.localScale.x;
        objFly.direction = (shooterDirection >= 0) ? Vector3.right : Vector3.left;
    }
}

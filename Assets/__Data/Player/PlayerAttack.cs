using __Data.Script;
using UnityEngine;

public class PlayerAttack : AbilityAttack
{
    [Header("Player Attack")]
    [SerializeField] protected float delay = 0.5f;
    [SerializeField] protected float countTime;

    protected override void Start()
    {
        base.Start();
        countTime = delay;
    }
    
    protected void FixedUpdate()
    {
        if (countTime > 0)
        {
            countTime -= Time.deltaTime;
            if (countTime <= 0)
                countTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (countTime <= 0f && animator.GetBool(AnimString.isAlive))
            {
                if (detectedAttack.Count > 0) 
                    MeleeAttack();
                else
                    RangedAttack();
                
                countTime = delay;
            }
        }
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
        if (countTime <= 0f && animator.GetBool(AnimString.isAlive))
        {
            if (detectedAttack.Count > 0) 
                MeleeAttack();
            else
                RangedAttack();
            
            countTime = delay;
        }
    }
    
    public void PlayerSenderDamage()
    {
        foreach (Collider2D enemy in detectedAttack.ToArray())
        {
            EnemyCtrl enemyCtrl = enemy.GetComponentInChildren<EnemyCtrl>();
            
            enemyCtrl.HitEnemy();
        }
    }
}

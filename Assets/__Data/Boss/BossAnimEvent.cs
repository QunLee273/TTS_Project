using System.Collections;
using __Data;
using UnityEngine;

public class BossAnimEvent : GameBehaviour
{
    [Header("Boss Animation Event")]
    [SerializeField] protected BossAttack bossAttack;
    public BossAttack BossAttack => bossAttack;
    [SerializeField] protected BossAbility bossAbility;
    public BossAbility BossAbility => bossAbility;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        LoadBossAtk();
        LoadBossAbility();
    }

    private void LoadBossAtk()
    {
        if (bossAttack != null) return;
        GameObject obj = transform.parent.gameObject;
        bossAttack = obj.GetComponentInChildren<BossAttack>();
        Debug.LogWarning(transform.name + ": LoadBossAtk", gameObject);
    }
    
    private void LoadBossAbility()
    {
        if (bossAbility != null) return;
        GameObject obj = transform.parent.gameObject;
        bossAbility = obj.GetComponentInChildren<BossAbility>();
        Debug.LogWarning(transform.name + ": LoadBossAbility", gameObject);
    }

    public void BossMeleeDam()
    {
        BossAttack.BossMeleeAttack();
    }
    
    public void BossRangedDam()
    {
        BossAttack.BossRangedAttack();
    }

    public void BossLazerDamStart()
    {
        bossAbility.AbilityLazer.StartLazing();
    }
    
    public void BossLazerDamEnd()
    {
        bossAbility.AbilityLazer.EndLazing();
    }
    
    public void BossShield()
    {
        bossAbility.BossShield.UseShield();
    }
    
    public void BossHealing()
    {
        bossAbility.BossHealing.UseHealing();
    }
    
    public void BossMeteor()
    {
        bossAbility.BossMeteor.StartMeteorRain();
    }

    
}

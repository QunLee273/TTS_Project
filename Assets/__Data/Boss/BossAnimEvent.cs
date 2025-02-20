using __Data;
using UnityEngine;

public class BossAnimEvent : GameBehaviour
{
    [Header("Boss Animation Event")]
    [SerializeField] protected BossAttack bossAttack;
    public BossAttack BossAttack => bossAttack;
    [SerializeField] protected AbilityLazer abilityLazer;
    public AbilityLazer AbilityLazer => abilityLazer;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        LoadBossAtk();
        LoadAbilityLazer();
    }

    private void LoadBossAtk()
    {
        if (bossAttack != null) return;
        GameObject obj = transform.parent.gameObject;
        bossAttack = obj.GetComponentInChildren<BossAttack>();
        Debug.LogWarning(transform.name + ": LoadBossAtk", gameObject);
    }
    
    private void LoadAbilityLazer()
    {
        if (abilityLazer != null) return;
        GameObject obj = transform.parent.gameObject;
        abilityLazer = obj.GetComponentInChildren<AbilityLazer>();
        Debug.LogWarning(transform.name + ": LoadAbilityLazer", gameObject);
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
        abilityLazer.StartLazing();
    }
    
    public void BossLazerDamEnd()
    {
        abilityLazer.EndLazing();
    }
}

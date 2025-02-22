using UnityEngine;

public class BossAbility : ObjAbility
{
    [Header("Boss Ability")]
    [SerializeField] protected AbilityLazer abilityLazer;
    public AbilityLazer AbilityLazer => abilityLazer;
    [SerializeField] protected BossShield bossShield;
    public BossShield BossShield => bossShield;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAbilityLazer();
        LoadBossShield();
    }

    private void LoadAbilityLazer()
    {
        if (abilityLazer != null) return;
        abilityLazer = gameObject.GetComponentInChildren<AbilityLazer>();
        Debug.LogWarning(transform.name + ": LoadAbilityLazer", gameObject);
    }
    
    private void LoadBossShield()
    {
        if (bossShield != null) return;
        bossShield = transform.parent.GetComponentInChildren<BossShield>();
        Debug.LogWarning(transform.name + ": LoadBossShield", gameObject);
    }
}

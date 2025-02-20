using UnityEngine;

public class BossAbility : ObjAbility
{
    [Header("Boss Ability")]
    [SerializeField] protected AbilityLazer abilityLazer;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAbilityLazer();
    }

    private void LoadAbilityLazer()
    {
        if (abilityLazer != null) return;
        abilityLazer = gameObject.GetComponentInChildren<AbilityLazer>();
        Debug.LogWarning(transform.name + ": LoadAbilityLazer", gameObject);
    }
    
    
}

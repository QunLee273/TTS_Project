using UnityEngine;

public class PlayerAbility : ObjAbility
{
    [Header("Player Ability")]
    [SerializeField] protected AbilityDash abilityDash;
    public AbilityDash AbilityDash => abilityDash;
    [SerializeField] protected AbilityInvisible abilityInvisible;
    public AbilityInvisible AbilityInvisible => abilityInvisible;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAbilityDash();
        LoadAbilityInvisible();
    }

    private void LoadAbilityDash()
    {
        if (abilityDash != null) return;
        abilityDash = GetComponentInChildren<AbilityDash>();
        Debug.LogWarning(transform.name + ": LoadAbilityDash", gameObject);
    }
    
    private void LoadAbilityInvisible()
    {
        if (abilityInvisible != null) return;
        abilityInvisible = GetComponentInChildren<AbilityInvisible>();
        Debug.LogWarning(transform.name + ": LoadAbilityInvisible", gameObject);
    }
}

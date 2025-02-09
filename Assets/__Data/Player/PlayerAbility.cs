using UnityEngine;

public class PlayerAbility : ObjAbility
{
    [Header("Player Ability")]
    [SerializeField] protected AbilityDash abilityDash;
    public AbilityDash AbilityDash => abilityDash;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAbilityDash();
    }

    private void LoadAbilityDash()
    {
        if (abilityDash != null) return;
        abilityDash = GetComponentInChildren<AbilityDash>();
        Debug.LogWarning(transform.name + ": LoadAbilityDash", gameObject);
    }
}

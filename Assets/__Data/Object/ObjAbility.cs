using __Data;
using UnityEngine;

public class ObjAbility : GameBehaviour
{
    [Header("ObjAbility")]
    [SerializeField] protected AbilityAttack abilityAttack;
    public AbilityAttack AbilityAttack => abilityAttack;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAbilityAttack();
    }

    private void LoadAbilityAttack()
    {
        if (abilityAttack != null) return;
        abilityAttack = GetComponentInChildren<AbilityAttack>();
        Debug.LogWarning(transform.name + ": LoadAbilityAttack", gameObject);
    }
}

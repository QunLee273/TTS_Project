using __Data;
using UnityEngine;

public class PlayerAnimEvent : GameBehaviour
{
    [Header("Player Animation Event")]
    [SerializeField] protected PlayerAttack playerAttack;
    public PlayerAttack PlayerAttack => playerAttack;
    [SerializeField] protected AbilityDash abilityDash;
    public AbilityDash AbilityDash => abilityDash;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        LoadPlayerAtk();
        LoadAbilityDash();
    }

    private void LoadPlayerAtk()
    {
        if (playerAttack != null) return;
        GameObject obj = transform.parent.gameObject;
        playerAttack = obj.GetComponentInChildren<PlayerAttack>();
        Debug.LogWarning(transform.name + ": LoadPlayerAtk", gameObject);
    }
    
    private void LoadAbilityDash()
    {
        if (abilityDash != null) return;
        GameObject obj = transform.parent.gameObject;
        abilityDash = obj.GetComponentInChildren<AbilityDash>();
        Debug.LogWarning(transform.name + ": LoadAbilityDash", gameObject);
    }

    private void Respawn()
    {
        PlayerController.Instance.Respawn();
    }

    public void MeleeDamage()
    {
        playerAttack.StartMelee();
    }

    public void RangedDamage()
    {
        playerAttack.StartRanged();
    }

    public void DashDamage()
    {
        abilityDash.DashAttack();
    }
}

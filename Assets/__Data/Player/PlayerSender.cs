using __Data;
using UnityEngine;

public class PlayerSender : DamageSender
{
    [Header("Player Sender")]
    [SerializeField] protected PlayerAttack playerAttack;
    public PlayerAttack PlayerAttack => playerAttack;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        LoadPlayerAtk();
    }

    private void LoadPlayerAtk()
    {
        if (playerAttack != null) return;
        playerAttack = objController.ObjAbility.GetComponentInChildren<PlayerAttack>();
        Debug.LogWarning(transform.name + ": LoadPlayerAtk", gameObject);
    }

    public void TriggerDealDamage()
    {
        playerAttack.PlayerSenderDamage();
    }
}

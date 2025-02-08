using __Data;
using UnityEngine;

public class PlayerAnimEvent : GameBehaviour
{
    [Header("Player Animation Event")]
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
        GameObject obj = transform.parent.gameObject;
        playerAttack = obj.GetComponentInChildren<PlayerAttack>();
        Debug.LogWarning(transform.name + ": LoadPlayerAtk", gameObject);
    }

    public void MeleeDamage()
    {
        playerAttack.StartMelee();
    }

    public void RangedDamage()
    {
        playerAttack.StartRanged();
    }

    public void EndAttack()
    {
        playerAttack.EndAttacking();
    }
}

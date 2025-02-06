using UnityEngine;

public class EnemySender : DamageSender
{
    [Header("Enemy Sender")]
    [SerializeField] protected EnemyAttack enemyAttack;
    public EnemyAttack EnemyAttack => enemyAttack;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        LoadEnemyAtk();
    }

    private void LoadEnemyAtk()
    {
        if (enemyAttack != null) return;
        enemyAttack = objController.ObjAbility.GetComponentInChildren<EnemyAttack>();
        Debug.LogWarning(transform.name + ": LoadEnemyAtk", gameObject);
    }

    public void TriggerDealDamage()
    {
        EnemyAttack.DealDamage();
    }
}

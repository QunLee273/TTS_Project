using __Data;
using UnityEngine;

public class EnemyAnimEvent : GameBehaviour
{
    [Header("Enemy Animation Event")]
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
        GameObject obj = transform.parent.gameObject;
        enemyAttack = obj.GetComponentInChildren<EnemyAttack>();
        Debug.LogWarning(transform.name + ": LoadEnemyAtk", gameObject);
    }

    public void EnemyMeleeDam()
    {
        EnemyAttack.EnemyMeleeSenderDam();
    }
    
    public void EnemyRangedDam()
    {
        EnemyAttack.EnemyRangedSenderDam();
    }
}

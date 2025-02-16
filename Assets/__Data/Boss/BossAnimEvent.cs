using __Data;
using UnityEngine;

public class BossAnimEvent : GameBehaviour
{
    [Header("Boss Animation Event")]
    [SerializeField] protected BossAttack bossAttack;
    public BossAttack BossAttack => bossAttack;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        LoadBossAtk();
    }

    private void LoadBossAtk()
    {
        if (bossAttack != null) return;
        GameObject obj = transform.parent.gameObject;
        bossAttack = obj.GetComponentInChildren<BossAttack>();
        Debug.LogWarning(transform.name + ": LoadBossAtk", gameObject);
    }

    public void BossMeleeDam()
    {
        BossAttack.BossMeleeAttack();
    }
    
    public void BossRangedDam()
    {
        BossAttack.BossRangedAttack();
    }
}

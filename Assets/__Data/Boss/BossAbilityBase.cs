using System.Collections;
using UnityEngine;
using __Data;

public abstract class BossAbilityBase : AbilityAbstract
{
    [Header("Boss Ability Base")]
    [SerializeField] protected int priority = 0;       
    [SerializeField] protected float cooldown = 5f;    
    [Range(-1f, 1f)]
    [SerializeField] protected float hpThreshold = -1f; // Chỉ dùng khi HP% <= threshold (-1 = ignore)

    protected float lastUsed = -999f;
    protected BossCtrl bossCtrl;
    protected BossReceiver bossReceiver;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        bossCtrl = BossCtrl.Instance;
        bossReceiver = BossReceiver.Instance;
    }

    public virtual bool CanUse()
    {
        if (Time.time - lastUsed < cooldown) return false;

        if (hpThreshold >= 0f && bossReceiver != null)
        {
            float hpPercent = (float)bossReceiver.Lifes / bossReceiver.MaxLifes;
            if (hpPercent > hpThreshold) return false;
        }

        return ExtraCondition();
    }

    protected virtual bool ExtraCondition() => true;

    public IEnumerator Use()
    {
        lastUsed = Time.time;
        yield return Perform();
    }

    public IEnumerator Execute() => Use();

    protected abstract IEnumerator Perform();
}
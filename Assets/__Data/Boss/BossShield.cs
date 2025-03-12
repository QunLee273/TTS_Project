using System;
using System.Collections;
using __Data;
using __Data.Script;
using UnityEngine;

public class BossShield : AbilityAbstract
{
    [Header("Boss Shield")]
    [SerializeField] protected GameObject shield;
    [SerializeField] protected float timeShield = 10f;
    
    [SerializeField] protected float thresholdHp = 0.75f;
    [SerializeField] protected float currentHp;
    [SerializeField] protected float maxHp;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadShield();
    }

    protected override void Start()
    {
        base.Start();
        maxHp = BossReceiver.Instance.MaxLifes;
    }

    protected void Update()
    {
        if (BossCtrl.Instance.isUsingAbility) return;
        currentHp = BossReceiver.Instance.Lifes;
        float hpPercent = currentHp / maxHp;
        if (hpPercent <= thresholdHp && thresholdHp > 0)
        {
            animator.SetBool(AnimString.spells, true);
            animator.SetBool(AnimString.shield, true);
            animator.SetBool(AnimString.canMove, false);
            thresholdHp -= 0.25f;
        }
    }
    
    private void LoadShield()
    {
        if(shield != null) return;
        shield = GameObject.Find("ShieldEffect");
        shield.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadShield", gameObject);
    }

    public void UseShield()
    {
        BossCtrl.Instance.isUsingAbility = true;
        animator.SetBool(AnimString.spells, false);
        animator.SetBool(AnimString.shield, false);
        animator.SetBool(AnimString.canMove, true);
        shield.SetActive(true);
        BossReceiver.Instance.IsInvulnerable = true;
        StartCoroutine(ActivateShield());
    }

    IEnumerator ActivateShield()
    {
        BossCtrl.Instance.isUsingAbility = false;
        yield return new WaitForSeconds(timeShield);
        shield.SetActive(false);
        BossReceiver.Instance.IsInvulnerable = false;
    }
}

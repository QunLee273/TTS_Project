using System;
using System.Collections;
using __Data;
using __Data.Script;
using UnityEngine;

public class BossShield : GameBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject shield;
    [SerializeField] protected float timeShield = 10f;
    
    [SerializeField] protected float thresholdHp = 0.8f;
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
        maxHp = BossReceiver.Instance.Lifes;
    }

    protected void FixedUpdate()
    {
        currentHp = BossReceiver.Instance.Lifes;
        float hpPercent = currentHp / maxHp;
        if (hpPercent <= thresholdHp && thresholdHp > 0)
        {
            animator.SetBool(AnimString.spells, true);
            animator.SetTrigger(AnimString.shieldTrigger);
            animator.Play("Boss_Shield");
            thresholdHp -= 0.2f;
        }
    }

    public void UseShield()
    {
        animator.SetBool(AnimString.spells, false);
        shield.SetActive(true);
        BossReceiver.Instance.IsInvulnerable = true;
        StartCoroutine(ActivateShield());
    }

    private void LoadShield()
    {
        if(shield != null) return;
        shield = GameObject.Find("ShieldEffect");
        shield.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadShield", gameObject);
    }

    IEnumerator ActivateShield()
    {
        yield return new WaitForSeconds(timeShield);
        shield.SetActive(false);
        BossReceiver.Instance.IsInvulnerable = false;
    }
}

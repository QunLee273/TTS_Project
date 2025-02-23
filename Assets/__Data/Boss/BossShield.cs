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
        LoadAnimation();
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
            animator.SetBool(AnimString.shield, true);
            animator.SetBool(AnimString.canMove, false);
            thresholdHp -= 0.2f;
        }
    }
    
    private void LoadShield()
    {
        if(shield != null) return;
        shield = GameObject.Find("ShieldEffect");
        shield.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadShield", gameObject);
    }
    
    private void LoadAnimation()
    {
        if(animator != null) return;
        animator = transform.parent.parent.GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimation", gameObject);
    }

    public void UseShield()
    {
        animator.SetBool(AnimString.spells, false);
        animator.SetBool(AnimString.shield, false);
        animator.SetBool(AnimString.canMove, true);
        shield.SetActive(true);
        BossReceiver.Instance.IsInvulnerable = true;
        StartCoroutine(ActivateShield());
    }

    IEnumerator ActivateShield()
    {
        yield return new WaitForSeconds(timeShield);
        shield.SetActive(false);
        BossReceiver.Instance.IsInvulnerable = false;
    }
}

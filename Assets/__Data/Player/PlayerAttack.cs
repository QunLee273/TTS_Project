using System;
using __Data.Script;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttack : AbilityAttack
{
    [SerializeField] protected float delay = 0.5f;
    [SerializeField] protected float countTime;

    protected override void Start()
    {
        base.Start();
        countTime = delay;
    }
    protected void FixedUpdate()
    {
        if (countTime > 0)
        {
            countTime -= Time.deltaTime;
            if (countTime <= 0)
                countTime = 0;
        }
    }

    private void OnAttack()
    {
        animator.SetTrigger(AnimString.attackTrigger);
        animator.Play("Player_Atk1");
    }

    public void OnClick()
    {
        if (countTime <= 0f)
        {
            OnAttack();
            
            countTime = delay;
        }
    }
}

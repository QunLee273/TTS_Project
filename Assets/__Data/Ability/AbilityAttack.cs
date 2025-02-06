using System;
using __Data;
using __Data.Script;
using UnityEngine;

public class AbilityAttack : GameBehaviour
{
    [Header("AbilityAttack")]
    [SerializeField] protected Rigidbody2D rb;
    public Rigidbody2D Rb => rb;
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        
        LoadRigidBody2D();
        LoadAnimator();
    }

    

    private void LoadRigidBody2D()
    {
        if (rb != null) return;
        rb = gameObject.GetComponentInParent<Rigidbody2D>();
        Debug.LogWarning(transform.name + ": LoadRigidBody2D", gameObject);
    }

    private void LoadAnimator()
    {
        if (animator != null) return;
        animator = transform.parent.parent.Find("Model").GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    
}

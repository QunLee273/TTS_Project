using System;
using System.Collections.Generic;
using __Data;
using __Data.Script;
using UnityEngine;

public class AbilityAttack : GameBehaviour
{
    [Header("Ability Attack")]
    [SerializeField] protected Rigidbody2D rb;
    public Rigidbody2D Rb => rb;
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;
    
    [SerializeField] protected Collider2D col;
    [SerializeField] protected List<Collider2D> detectedAttack = new List<Collider2D>();
    
    [SerializeField] protected bool hasTarget;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        
        LoadRigidBody2D();
        LoadAnimator();
        LoadCollider2D();
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

    private void LoadCollider2D()
    {
        if (col != null) return;
        col = GetComponent<Collider2D>();
        Debug.LogWarning(transform.name + ": LoadCollider2D", gameObject);
    }
    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        detectedAttack.Add(other);
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        detectedAttack.Remove(other);
    }
}

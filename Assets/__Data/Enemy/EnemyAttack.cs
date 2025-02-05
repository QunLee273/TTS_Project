using System;
using System.Collections.Generic;
using __Data.Script;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAttack : AbilityAttack
{
    [Header("EnemyAttack")]
    [SerializeField] protected Collider2D col;
    
    [SerializeField] protected List<Collider2D> detectedAttack = new List<Collider2D>();
    
    [SerializeField] protected bool hasTarget;

    public bool HasTarget
    {
        get => hasTarget;
        set
        {
            hasTarget = value;
            animator.SetBool(AnimString.hasTarget, value);
        }
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        
        LoadCollider2D();
    }

    protected void FixedUpdate()
    {
        HasTarget = detectedAttack.Count > 0;
    }

    private void LoadCollider2D()
    {
        if (col != null) return;
        col = GetComponent<Collider2D>();
        Debug.LogWarning(transform.name + ": LoadCollider2D", gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        detectedAttack.Add(collider);
    }

    protected void OnTriggerExit2D(Collider2D collider)
    {
        detectedAttack.Remove(collider);
    }
}

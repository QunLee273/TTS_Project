using System;
using System.Collections.Generic;
using __Data;
using __Data.Script;
using UnityEngine;

public class AbilityAttack : AbilityAbstract
{
    [Header("Ability Attack")]
    [SerializeField] protected Collider2D col;
    [SerializeField] protected List<Collider2D> detectedAttack = new List<Collider2D>();
    public List<Collider2D> DetectedAttack => detectedAttack;
    
    [SerializeField] protected bool hasTarget;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        
        LoadCollider2D();
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

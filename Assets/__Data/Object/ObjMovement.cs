using __Data;
using UnityEngine;

public class ObjMovement : GameBehaviour
{
    [Header("ObjMovement")]
    [SerializeField] protected float moveSpeed = 5f;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }
    
    [SerializeField] protected Rigidbody2D rb;
    public Rigidbody2D Rb => rb;
    [SerializeField] protected Collider2D col;
    public Collider2D Col => col;
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        
        LoadRigidBody2D();
        LoadCol2D();
        LoadAnimator();
    }

    private void LoadRigidBody2D()
    {
        if (rb != null) return;
        rb = GetComponentInParent<Rigidbody2D>();
        Debug.LogWarning(transform.name + ": LoadRigidBody2D", gameObject);
    }
    
    private void LoadCol2D()
    {
        if (col != null) return;
        col = GetComponentInParent<Collider2D>();
        Debug.LogWarning(transform.name + ": LoadCol2D", gameObject);
    }

    private void LoadAnimator()
    {
        if (animator != null) return;
        GameObject objParent = transform.parent.gameObject;
        animator = objParent.GetComponentInChildren<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }
}

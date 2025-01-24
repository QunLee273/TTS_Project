using __Data;
using UnityEngine;

public class ObjMovement : GameBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;
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
        rb = GetComponentInParent<Rigidbody2D>();
        Debug.LogWarning(transform.name + ": LoadRigidBody2D", gameObject);
    }

    private void LoadAnimator()
    {
        if (animator != null) return;
        GameObject objParent = transform.parent.gameObject;
        animator = objParent.GetComponentInChildren<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }
}

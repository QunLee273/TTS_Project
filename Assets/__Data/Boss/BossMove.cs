using __Data.Script;
using UnityEngine;

public class BossMove : ObjMovement
{
    [Header("Boss Move")] [SerializeField] protected Transform target;
    [SerializeField] protected float stopDistance = 1f;

    public bool CanMove => animator.GetBool(AnimString.canMove);

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTarget();
    }

    private void LoadTarget()
    {
        if (target != null) return;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.LogWarning(transform.name + ": LoadTarget", gameObject);
    }

    protected void FixedUpdate()
    {
        if (target == null) return;
        if (CanMove == false)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        
        MoveToTarget();
    }
    
    protected virtual void MoveToTarget()
    {
        float distance = Vector2.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

            if (direction.x > 0)
                transform.parent.localScale = new Vector3(1, 1, 1);
            else if (direction.x < 0)
                transform.parent.localScale = new Vector3(-1, 1, 1);
        }
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }
}

using __Data.Script;
using UnityEngine;

public class EnemyCtrl : ObjController
{
    [Header("Enemy Controller")]
    [SerializeField] protected bool isAlive = true; 
    public bool IsAlive
    {
        get => isAlive;
        private set
        {
            isAlive = value;
            ObjMovement.Animator.SetBool(AnimString.isAlive, isAlive);
            ObjMovement.Animator.SetBool(AnimString.canMove, isAlive);
        }
    }
    
    protected override bool IsDebugEnabled => true;

    protected void Update()
    {
        CheckIsAlive();
    }

    private void CheckIsAlive()
    {
        if (DamageReceiver.IsDead())
        {
            IsAlive = false;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            Collider2D[] collider2Ds = GetComponentsInChildren<Collider2D>(true);
            foreach (Collider2D col in collider2Ds)
            {
                col.enabled = false;
            }
        }
    }
    
    protected override string GetObjectTypeString()
    {
        return ObjectType.Enemy.ToString();
    }
}

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
    
    public void HitEnemy()
    {
        if (!IsAlive) return;
        
        DamageReceiver.Deduct(1);
        Debug.Log(DamageReceiver.Lifes);
        if (DamageReceiver.IsDead())
            IsAlive = false;
    }
    
    protected override string GetObjectTypeString()
    {
        return ObjectType.Enemy.ToString();
    }
}

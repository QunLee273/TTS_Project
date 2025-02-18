using __Data.Script;
using UnityEngine;

public class BossCtrl : ObjController
{
    [Header("Boss Control")]
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
    
    protected void Update()
    {
        CheckIsAlive();
    }

    private void CheckIsAlive()
    {
        if (DamageReceiver.IsDead())
            IsAlive = false;
    }
    protected override string GetObjectTypeString()
    {
        return ObjectType.Boss.ToString();
    }
}

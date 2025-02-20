using __Data;
using __Data.Script;
using UnityEngine;

public class BreakablesCtrl : ObjController
{
    [Header("Breakables Ctrl")]
    [SerializeField] protected bool isAlive = true; 
    public bool IsAlive
    {
        get => isAlive;
        private set
        {
            isAlive = value;
            Model.gameObject.GetComponent<Animator>().SetBool(AnimString.isAlive, isAlive);
        }
    }
    
    protected override bool IsDebugEnabled => false;

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
        return ObjectType.Breakables.ToString();
    }
}

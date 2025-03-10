using __Data;
using __Data.Script;
using UnityEngine;

public class BreakablesCtrl : ObjController
{
    [Header("Breakables Ctrl")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected bool isAlive = true; 
    public bool IsAlive
    {
        get => isAlive;
        private set
        {
            isAlive = value;
            anim.SetBool(AnimString.isAlive, isAlive);
        }
    }
    
    protected override bool IsDebugEnabled => false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAnim();
    }

    private void LoadAnim()
    {
        if (anim != null) return;
        anim = GetComponentInChildren<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnim", gameObject);
    }

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
            rb.bodyType = RigidbodyType2D.Static;
            Collider2D[] collider2Ds = GetComponentsInChildren<Collider2D>(true);
            foreach (Collider2D col in collider2Ds)
                col.enabled = false;
        }
    }
    protected override string GetObjectTypeString()
    {
        return ObjectType.Breakables.ToString();
    }
}

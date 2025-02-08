using __Data;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class DamageReceiver : GameBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected CapsuleCollider2D capsuleCollider;
    [SerializeField] protected ObjController objController;
    [SerializeField] protected int lifes = 1;
    [SerializeField] protected bool isDead;

    public int Lifes
    {
        get => lifes;
        set => lifes = value;
    }

    protected override void OnEnable()
    {
        Reborn();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        
        LoadCollider();
        LoadObjCtrl();
    }

    private void LoadCollider()
    {
        if (capsuleCollider != null) return;
        capsuleCollider = GetComponentInParent<CapsuleCollider2D>();
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }

    private void LoadObjCtrl()
    {
        if (objController != null) return;
        objController = GetComponentInParent<ObjController>();
        Debug.Log(transform.name + ": LoadObjCtrl", gameObject);
    }

    protected virtual void Reborn()
    {
        isDead = false;
    }

    public  void Add(int add)
    {
        if (isDead) return;

        lifes += add;
    }

    public virtual void Deduct(int deduct)
    {
        if (isDead) return;

        lifes -= deduct;
        
        CheckIsDead();
    }

    public bool IsDead()
    {
        return lifes <= 0;
    }

    private void CheckIsDead()
    {
        if (!IsDead()) return;
        isDead = true;
        OnDead();
    }

    protected abstract void OnDead();
}

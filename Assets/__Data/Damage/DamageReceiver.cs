using __Data;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class DamageReceiver : GameBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected Collider2D colli2d;
    [SerializeField] protected ObjController objController;
    [SerializeField] protected int lifes;
    [SerializeField] protected int maxLifes;
    [SerializeField] protected bool isDead;
    public bool Dead
    {
        get => isDead;
        set => isDead = value;
    }

    public int Lifes
    {
        get => lifes;
        set => lifes = value;
    }
    
    public int MaxLifes
    {
        get => maxLifes;
        set => maxLifes = value;
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
        if (colli2d != null) return;
        colli2d = GetComponentInParent<Collider2D>();
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
        lifes = maxLifes;
        isDead = false;
    }

    public void Add(int add)
    {
        if (isDead) return;

        lifes += add;
    }

    public virtual void Deduct(int deduct)
    {
        if (isDead) return;

        lifes -= deduct;
        if (lifes < 0) lifes = 0;
        
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

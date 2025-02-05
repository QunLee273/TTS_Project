using __Data;
using UnityEngine;

public abstract class DamageReceiver : GameBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected CapsuleCollider2D capsuleCollider;
    [SerializeField] protected ObjController objController;
    [SerializeField] protected int hp = 1;
    [SerializeField] protected bool isDead = false;

    public int Hp
    {
        get => hp;
        set => hp = value;
    }

    protected override void OnEnable()
    {
        this.Reborn();
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

    public  void Reborn()
    {
        hp = objController.GameObjectSo.life;
        isDead = false;
    }

    public  void Add(int add)
    {
        if (this.isDead) return;

        this.hp += add;
    }

    public  void Deduct(int deduct)
    {
        if (this.isDead) return;

        this.hp -= deduct;
        
        CheckIsDead();
    }

    private bool IsDead()
    {
        return this.hp <= 0;
    }

    private void CheckIsDead()
    {
        if (!this.IsDead()) return;
        this.isDead = true;
        this.OnDead();
    }

    protected abstract void OnDead();
}

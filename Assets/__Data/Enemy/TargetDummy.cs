using __Data;
using __Data.Script;
using UnityEngine;

public class TargetDummy : GameBehaviour
{
    [SerializeField] protected Collider2D colli;
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCollider();
        LoadAnim();
    }

    private void LoadCollider()
    {
        if (colli != null) return;
        colli = GetComponent<Collider2D>();
        colli.isTrigger = true;
        Debug.LogWarning(transform.name + ": LoadCollider", gameObject);
    }
    
    private void LoadAnim()
    {
        if (animator != null) return;
        animator = GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnim", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletCtrl bulletCtrl = collision.transform.parent.GetComponent<BulletCtrl>();
        if (bulletCtrl == null) return;
        if (bulletCtrl.Shooter.name == "Player")
        {
            animator.SetBool(AnimString.hit, true);
            Destroy(bulletCtrl.gameObject);
            CreateImpactFX();
        }
    }
    
    public void ResetHit()
    {
        animator.SetBool(AnimString.hit, false);
    }
    
    public virtual void CreateImpactFX()
    {
        string fxName = GetImpactFX();

        Vector3 hitPos = transform.position;
        Transform fxImpact = FXSpawner.Instance.Spawn(fxName, hitPos, Quaternion.identity);
        fxImpact.gameObject.SetActive(true);
    }

    protected virtual string GetImpactFX()
    {
        return FXSpawner.hit_1;
    }
}

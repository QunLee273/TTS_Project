using __Data.Script;
using UnityEngine;

public class BossReceiver : DamageReceiver
{
    protected override void Reborn()
    {
        base.Reborn();
        lifes = objController.GameObjectSo.life;
    }
    
    protected override void OnDead()
    {
        Debug.Log("Boss OnDead");
        Animator animator = transform.parent.GetComponentInChildren<Animator>();
        animator.SetBool(AnimString.isAlive, false);
        animator.SetBool(AnimString.canMove, false);
    }
}

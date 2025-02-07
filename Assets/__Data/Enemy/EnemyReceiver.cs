using UnityEngine;

public class EnemyReceiver : DamageReceiver
{
    protected override void Reborn()
    {
        base.Reborn();
        lifes = objController.GameObjectSo.life;
    }
    
    protected override void OnDead()
    {
        Debug.Log("Enemy Dead");
    }
}

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
    }
}

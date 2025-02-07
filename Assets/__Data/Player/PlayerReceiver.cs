using UnityEngine;

public class PlayerReceiver : DamageReceiver
{
    protected override void Reborn()
    {
        base.Reborn();
        lifes = objController.GameObjectSo.life;
    }

    protected override void OnDead()
    {
        Debug.Log("Player Dead");
    }
}

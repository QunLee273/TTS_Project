using UnityEngine;

public class EnemyReceiver : DamageReceiver
{
    protected override void OnDead()
    {
        Debug.Log("Enemy Dead");
    }
}

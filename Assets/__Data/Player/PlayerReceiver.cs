using UnityEngine;

public class PlayerReceiver : DamageReceiver
{
    protected override void OnDead()
    {
        Debug.Log("Player Dead");
    }
}

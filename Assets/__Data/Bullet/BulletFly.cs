using UnityEngine;

public class BulletFly : ObjFly
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.moveSpeed = 15f;
    }
}

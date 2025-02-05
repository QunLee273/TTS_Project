using UnityEngine;

public class EnemyCtrl : ObjController
{
    protected override string GetObjectTypeString()
    {
        return ObjectType.Enemy.ToString();
    }
}

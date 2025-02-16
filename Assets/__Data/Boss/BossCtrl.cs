using UnityEngine;

public class BossCtrl : ObjController
{
    protected override string GetObjectTypeString()
    {
        return ObjectType.Boss.ToString();
    }
}

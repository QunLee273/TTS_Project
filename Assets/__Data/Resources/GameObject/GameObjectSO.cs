using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObject", menuName = "ScriptableObjects/GameObject")]
public class GameObjectSO : ScriptableObject
{
    public string objName = "Game Object";
    public ObjectType objType;
    public int life = 0;
}

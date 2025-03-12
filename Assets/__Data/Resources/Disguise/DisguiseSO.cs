using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Disguise", menuName = "ScriptableObjects/Disguise")]
public class DisguiseSO : ScriptableObject
{
    public List<DisguiseData> disguiseData;
}

[System.Serializable]
public class DisguiseData
{
    public string disguiseName;
    public Sprite disguiseIcon;
    public int price;
    public bool isPurchased;
}

using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SkillData
{
    public string skillName;
    public Sprite skillImage;
    public string descriptions;
    public String[] effects; 
    public int currentLevel;
    public int maxLevel = 5;
    public int baseCost;
    public float costMultiplier;
    public Button[] levelButtons;
    
    public float GetUpgradeCost(int level)
    {
        return baseCost * (level + 1) * costMultiplier;
    }
}

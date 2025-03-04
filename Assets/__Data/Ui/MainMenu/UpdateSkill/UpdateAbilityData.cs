using __Data;
using __Data.Script;
using UnityEngine;

public class UpdateAbilityData : GameBehaviour
{
    [SerializeField] protected SkillData[] skills;
    public SkillData[] Skills => skills;

    public void LoadData()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i].currentLevel = PlayerPrefs.GetInt(PlayerPrefsString.SkillLevel_ + i, 0);
        }
    }

    public void SaveData()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            PlayerPrefs.SetInt(PlayerPrefsString.SkillLevel_ + i, skills[i].currentLevel);
        }
        PlayerPrefs.Save();
    }
    
    public void SetData(int lv)
    {
        for (int i = 0; i < skills.Length; i++)
        {
            PlayerPrefs.SetInt(PlayerPrefsString.SkillLevel_ + i, lv);
        }
        PlayerPrefs.Save();
    }
}

using System;
using __Data;
using __Data.Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradeSystem : UpdateSkill
{
    [Header("Skill upgrade system")]
    [SerializeField] protected Button btnUpdate;
    public Button BtnUpdate { get => btnUpdate; set => btnUpdate = value; }

    [SerializeField] protected TMP_Text txtEffect;
    public TMP_Text TxtEffect { get => txtEffect; set => txtEffect = value; }

    [SerializeField] protected TMP_Text txtCoin;
    public TMP_Text TxtCoin { get => txtCoin; set => txtCoin = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnUpdate();
        LoadTxtEffect();
        LoadTxtCoin();
    }

    private void LoadBtnUpdate()
    {
        if (btnUpdate != null) return;
        btnUpdate = transform.Find("BtnUpdate").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnUpdate", gameObject);
    }

    private void LoadTxtEffect()
    {
        if (txtEffect != null) return;
        txtEffect = GameObject.Find("TxtEffect").GetComponent<TMP_Text>();
        Debug.LogWarning(transform.name + ": LoadTxtEffect", gameObject);
    }

    private void LoadTxtCoin()
    {
        if (txtCoin != null) return;
        txtCoin = GameObject.Find("TxtCoin").GetComponent<TMP_Text>();
        Debug.LogWarning(transform.name + ": LoadTxtCoin", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        btnUpdate.onClick.AddListener(UpgradeSkill);
    }

    public void UpgradeSkill()
    {
        int selectedSkillIndex = skillSelect.selectedSkillIndex;
        if (selectedSkillIndex == -1) return;

        SkillData skillData = updateAbilityData.Skills[selectedSkillIndex];
        int playerGold = PlayerPrefs.GetInt(PlayerPrefsString.AmountCoins, 0);

        if (skillData.currentLevel < skillData.maxLevel && playerGold >= skillData.GetUpgradeCost(skillData.currentLevel))
        {
            PlayerPrefs.SetInt(PlayerPrefsString.AmountCoins, playerGold - (Int32)skillData.GetUpgradeCost(skillData.currentLevel));
            skillData.currentLevel++;

            updateAbilityData.SaveData();
            skillSelect.SelectSkill(selectedSkillIndex);
        }
        else
        {
            Debug.Log("Không đủ vàng hoặc đã đạt cấp tối đa!");
        }
    }
    
    public void HighlightSkillLevel(SkillData skill, int targetLevel)
    {
        for (int i = 0; i < skill.levelButtons.Length; i++)
        {
            skill.levelButtons[i].interactable = (i == targetLevel);
            if (i < targetLevel)
            {
                skill.levelButtons[i].interactable = true;
                Image buttonImage = skill.levelButtons[i].GetComponent<Image>();
                buttonImage.color = Color.yellow;
            }
        }
    }
    
    public void AssignLevelButtons(SkillData skill)
    {
        for (int i = 0; i < skill.levelButtons.Length; i++)
        {
            int level = i;
            skill.levelButtons[i].onClick.RemoveAllListeners();
            skill.levelButtons[i].onClick.AddListener(() => SelectLevel(skill, level));
        }
    }

    private void SelectLevel(SkillData skill, int level)
    {
        for (int i = 0; i < skill.levelButtons.Length; i++)
        {
            Image buttonImage = skill.levelButtons[i].GetComponent<Image>();
            buttonImage.color = (i == level) ? Color.green : (i < skill.currentLevel ? Color.yellow : Color.white);
        }

        txtCoin.text = skill.GetUpgradeCost(level).ToString();
        txtEffect.text = skill.effects[level];
        if (level < skill.currentLevel)
            btnUpdate.interactable = false;
        else
            btnUpdate.interactable = true;
        
    }
}

using __Data;
using __Data.Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelect : UpdateSkill
{
    [Header("Skill Select")]
    [SerializeField] protected Button btnDash;
    [SerializeField] protected Button btnSurvival;
    [SerializeField] protected Button btnHensojutsu;
    [SerializeField] protected TMP_Text txtDescription;
    [SerializeField] protected Image imageDescription;

    public int selectedSkillIndex;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnDash();
        LoadBtnSurvival();
        LoadBtnHensojutsu();
        LoadTxtDesc();
        LoadImageDesc();
    }

    private void LoadBtnDash()
    {
        if (btnDash != null) return;
        btnDash = GameObject.Find("BtnDash").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnDash", gameObject);
    }

    private void LoadBtnSurvival()
    {
        if (btnSurvival != null) return;
        btnSurvival = GameObject.Find("BtnSurvival").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnSurvival", gameObject);
    }

    private void LoadBtnHensojutsu()
    {
        if (btnHensojutsu != null) return;
        btnHensojutsu = GameObject.Find("BtnHensojutsu").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnHensojutsu", gameObject);
    }

    private void LoadTxtDesc()
    {
        if (txtDescription != null) return;
        txtDescription = GameObject.Find("TextInfor").GetComponent<TMP_Text>();
        Debug.LogWarning(transform.name + ": LoadTxtDesc", gameObject);
    }

    private void LoadImageDesc()
    {
        if (imageDescription != null) return;
        imageDescription = GameObject.Find("ImageInfor").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadImageDesc", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        // PlayerPrefs.SetInt(PlayerPrefsString.AmountCoins, 999999999);
        // updateAbilityData.SetData(0);
        updateAbilityData.LoadData();
        btnDash.onClick.Invoke();
        btnDash.onClick.AddListener(() =>SelectSkill(0));
        btnSurvival.onClick.AddListener(() =>SelectSkill(1));
        btnHensojutsu.onClick.AddListener(() =>SelectSkill(2));
    }

    public void SelectSkill(int skillIndex)
    {
        selectedSkillIndex = skillIndex;
        SkillData skillData = updateAbilityData.Skills[skillIndex];
        ResetSkillTreeUI(skillData);

        imageDescription.sprite = skillData.skillImage;
        txtDescription.text = skillData.descriptions;

        int targetLevel = skillData.currentLevel < skillData.maxLevel ? skillData.currentLevel : skillData.maxLevel - 1;
        skillUpgradeSystem.HighlightSkillLevel(skillData, targetLevel);
        skillUpgradeSystem.AssignLevelButtons(skillData);
    }
    
    private void ResetSkillTreeUI(SkillData skill)
    {
        for (int i = 0; i < skill.levelButtons.Length; i++)
        {
            Image buttonImage = skill.levelButtons[i].GetComponent<Image>();
            buttonImage.color = (i < skill.currentLevel) ? Color.yellow : Color.white;
        }

        skillUpgradeSystem.TxtEffect.text = "";
        skillUpgradeSystem.TxtCoin.text = "00";
        skillUpgradeSystem.BtnUpdate.interactable = false;
    }
}

using __Data;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSkill : GameBehaviour
{
    [SerializeField] protected Button btnBack;
    [SerializeField] protected SkillSelect skillSelect;
    public SkillSelect SkillSelect => skillSelect;
    [SerializeField] protected  UpdateAbilityData updateAbilityData;
    public UpdateAbilityData UpdateAbilityData => updateAbilityData;
    [SerializeField] protected SkillUpgradeSystem skillUpgradeSystem;
    public SkillUpgradeSystem SkillUpgradeSystem => skillUpgradeSystem;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnBack();
        LoadSkillSelect();
        LoadUpdateAbilityData();
        LoadSkillUpdateSystem();
    }
    
    private void LoadBtnBack()
    {
        if (btnBack != null) return;
        btnBack = GameObject.Find("BtnBackShopUpdate").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnBack", gameObject);
    }
    
    private void LoadSkillSelect()
    {
        if (skillSelect != null) return;
        skillSelect = transform.parent?.GetComponentInChildren<SkillSelect>();
        Debug.LogWarning(transform.name + ": LoadSkillSelect", gameObject);
    }

    private void LoadUpdateAbilityData()
    {
        if (updateAbilityData != null) return;
        updateAbilityData = transform.parent?.GetComponentInChildren<UpdateAbilityData>();
        Debug.LogWarning(transform.name + ": LoadUpdateAbilityData", gameObject);
    }

    private void LoadSkillUpdateSystem()
    {
        if (skillUpgradeSystem != null) return;
        skillUpgradeSystem = transform.parent?.GetComponentInChildren<SkillUpgradeSystem>();
        Debug.LogWarning(transform.name + ": LoadSkillUpdateSystem", gameObject);
    }
    
    protected override void Start()
    {
        base.Start();
        btnBack.onClick.AddListener(OnClickBack);
    }
    
    private void OnClickBack()
    {
        UICtrlMainMenu.Instance.ShopUpdate.SetActive(true);
        UICtrlMainMenu.Instance.UpdateSkill.SetActive(false);
    }
}

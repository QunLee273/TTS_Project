using __Data;
using UnityEngine;

public class UICtrlMainMenu : GameBehaviour
{
    private static UICtrlMainMenu _instance;
    public static UICtrlMainMenu Instance => _instance;
    
    [SerializeField] protected GameObject mainMenu;
    public GameObject MainMenu => mainMenu;
    
    [SerializeField] protected GameObject selectLevel;
    public GameObject SelectLevel => selectLevel;
    
    [SerializeField] protected GameObject settings;
    public GameObject Settings => settings;
    
    [SerializeField] protected GameObject shopUpdate;
    public GameObject ShopUpdate => shopUpdate;
    
    [SerializeField] protected GameObject updateSkill;
    public GameObject UpdateSkill => updateSkill;

    [SerializeField] protected GameObject shop;
    public GameObject Shop => shop;
    
    [SerializeField] protected GameObject hensojutsu;
    public GameObject Hensojutsu => hensojutsu;

    protected override void Awake()
    {
        base.Awake();
        if (UICtrlMainMenu._instance != null) Debug.LogError("Only 1 UICtrl allow to exist");
        UICtrlMainMenu._instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMainMenu();
        LoadSelectLevel();
        LoadSetting();
        LoadShopUpdate();
        LoadUpdateSkill();
        LoadShop();
        LoadHensojutsu();
    }

    private void LoadMainMenu()
    {
        if (mainMenu != null) return;
        mainMenu = transform.Find("MainMenu").gameObject;
        Debug.LogWarning(transform.name + ": LoadMainMenu", gameObject);
    }
    
    private void LoadSelectLevel()
    {
        if (selectLevel != null) return;
        selectLevel = transform.Find("SelectLevel").gameObject;
        Debug.LogWarning(transform.name + ": LoadSelectLevel", gameObject);
    }
    
    private void LoadSetting()
    {
        if (settings != null) return;
        settings = transform.Find("Setting").gameObject;
        Debug.LogWarning(transform.name + ": LoadSetting", gameObject);
    }

    private void LoadShopUpdate()
    {
        if (shopUpdate != null) return;
        shopUpdate = transform.Find("Shop&Update").gameObject;
        Debug.LogWarning(transform.name + ": LoadShopUpdate", gameObject);
    }

    private void LoadUpdateSkill()
    {
        if (updateSkill != null) return;
        updateSkill = transform.Find("UpdateSkill").gameObject;
        Debug.LogWarning(transform.name + ": LoadUpdateSkill", gameObject);
    }

    private void LoadShop()
    {
        if (shop != null) return;
        shop = transform.Find("Shop").gameObject;
        Debug.LogWarning(transform.name + ": LoadShop", gameObject);
    }

    private void LoadHensojutsu()
    {
        if (hensojutsu != null) return;
        hensojutsu = transform.Find("Hensojutsu").gameObject;
        Debug.LogWarning(transform.name + ": LoadHensojutsu", gameObject);
    }
}

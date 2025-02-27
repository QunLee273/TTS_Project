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
}

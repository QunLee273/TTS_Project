using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : LevelEndState
{
    [Header("Pause Menu")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingButton;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadResume();
        LoadSetting();
    }

    private void LoadResume()
    {
        if (resumeButton != null) return;
        resumeButton = GameObject.Find("BtnResume").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadResume", gameObject);
    }

    private void LoadSetting()
    {
        if (settingButton != null) return;
        settingButton = GameObject.Find("BtnSetting").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadSetting", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        resumeButton.onClick.AddListener(Resume);
        settingButton.onClick.AddListener(Setting);
    }
    
    private void Resume()
    {
        Time.timeScale = 1;
        UICtrlInGame.Instance.SafeArea.SetActive(true);
        UICenter.Instance.PauseMenu.SetActive(false);
    }

    private void Setting()
    {
        UICenter.Instance.Setting.SetActive(true);
        UICenter.Instance.PauseMenu.SetActive(false);
    }
}

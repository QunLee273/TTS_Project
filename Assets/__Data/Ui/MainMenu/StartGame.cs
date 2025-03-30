using __Data;
using __Data.Script;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : GameBehaviour
{
    [SerializeField] protected Button startGameButton;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnStartGame();
    }

    protected override void Start()
    {
        base.Start();
        startGameButton.onClick.AddListener(OnClick_StartGame);
        
        bool openSelectMap = PlayerPrefs.GetInt(PlayerPrefsString.OpenSelectMap, 0) == 1;

        if (openSelectMap)
        {
            PlayerPrefs.SetInt(PlayerPrefsString.OpenSelectMap, 0);
            PlayerPrefs.Save();
            OnClick_StartGame();
        }
    }

    private void LoadBtnStartGame()
    {
        if (startGameButton != null) return;
        startGameButton = transform.Find("BtnStart").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnStartGame", gameObject);
    }

    public void OnClick_StartGame()
    {
        UICtrlMainMenu.Instance.SelectLevel.SetActive(true);
        UICtrlMainMenu.Instance.MainMenu.SetActive(false);
    }
}

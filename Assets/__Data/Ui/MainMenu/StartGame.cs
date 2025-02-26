using __Data;
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
    }

    private void LoadBtnStartGame()
    {
        if (startGameButton != null) return;
        startGameButton = transform.Find("BtnStart").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnStartGame", gameObject);
    }

    public void OnClick_StartGame()
    {
        UICtrl.Instance.SelectLevel.SetActive(true);
        UICtrl.Instance.MainMenu.SetActive(false);
    }
}

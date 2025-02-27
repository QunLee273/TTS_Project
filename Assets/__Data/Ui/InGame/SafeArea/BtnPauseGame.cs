using __Data;
using UnityEngine;
using UnityEngine.UI;

public class BtnPauseGame : GameBehaviour
{
    [SerializeField] protected  Button btnPause;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnPause();
    }
    
    private void LoadBtnPause()
    {
        if (btnPause != null) return;
        btnPause = GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnPause", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        btnPause.onClick.AddListener(OnClickBtnPause);
    }

    private void OnClickBtnPause()
    {
        PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        UICenter.Instance.PauseMenu.SetActive(true);
        UICtrlInGame.Instance.SafeArea.SetActive(false);
    }
}

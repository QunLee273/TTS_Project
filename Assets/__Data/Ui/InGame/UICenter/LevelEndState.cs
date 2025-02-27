using __Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEndState : GameBehaviour
{
    [Header("Level End State")]
    [SerializeField] protected Button restartButton;
    [SerializeField] protected Button mainMenuButton;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRestart();
        LoadMainMenu();
    }

    private void LoadRestart()
    {
        if (restartButton != null) return;
        restartButton = GameObject.Find("BtnRestart").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadRestart", gameObject);
    }

    private void LoadMainMenu()
    {
        if (mainMenuButton != null) return;
        mainMenuButton = GameObject.Find("BtnMainMenu").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadMainMenu", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        restartButton.onClick.AddListener(Restart);
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    private void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}

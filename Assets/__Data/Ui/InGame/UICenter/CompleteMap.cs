using __Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteMap : LevelEndState
{
    [Header("Complete Map")]
    [SerializeField] protected Button nextLevelButton;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        if (nextLevelButton != null) return;
        nextLevelButton = GameObject.Find("BtnNextLevel").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadNextLevel", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        nextLevelButton.onClick.AddListener(NextLevel);
    }

    private void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

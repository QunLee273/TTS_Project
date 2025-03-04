using __Data;
using UnityEngine;
using UnityEngine.Serialization;

public class UICenter : GameBehaviour
{
    private static UICenter _instance;
    public static UICenter Instance => _instance;
    
    [SerializeField] protected GameObject pauseMenu;
    public GameObject PauseMenu => pauseMenu;
    
    [SerializeField] protected GameObject completeMap;
    public GameObject CompleteMap => completeMap;
    
    [SerializeField] protected GameObject youDead;
    public GameObject YouDead => youDead;
    
    [SerializeField] protected GameObject getMoreLife;
    public GameObject GetMoreLife => getMoreLife;

    protected override void Awake()
    {
        base.Awake();
        if (UICenter._instance != null) Debug.LogError("Only 1 UICtrl allow to exist");
        UICenter._instance = this;
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPauseMenu();
        LoadComplete();
        LoadYouDead();
        LoadGetMoreLife();
    }

    private void LoadPauseMenu()
    {
        if (pauseMenu != null) return;
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadPauseMenu", gameObject);
    }

    private void LoadComplete()
    {
        if (completeMap != null) return;
        completeMap = GameObject.Find("CompleteMap");
        completeMap.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadComplete", gameObject);
    }

    private void LoadYouDead()
    {
        if (youDead != null) return;
        youDead = GameObject.Find("PlayerDead");
        youDead.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadDead", gameObject);
    }

    private void LoadGetMoreLife()
    {
        if (getMoreLife != null) return;
        getMoreLife = GameObject.Find("GetMoreLife");
        getMoreLife.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadGetMoreLife", gameObject);
    }
}

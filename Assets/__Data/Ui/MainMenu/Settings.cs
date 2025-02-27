using __Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : GameBehaviour
{
    [SerializeField] protected Button btnMusic;
    [SerializeField] protected Button btnBack;
    
    private bool isMusic = true;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnMusic();
        LoadBtnBack();
    }

    protected override void Start()
    {
        base.Start();
        btnMusic.onClick.AddListener(OnClickMusic);
        btnBack.onClick.AddListener(OnClickBack);
    }

    private void LoadBtnMusic()
    {
        if (btnMusic != null) return;
        btnMusic = transform.Find("BtnMusic").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnMusic", gameObject);
    }

    private void LoadBtnBack()
    {
        if (btnBack != null) return;
        btnBack = transform.Find("BtnBack").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnBack", gameObject);
    }

    private void OnClickMusic()
    {
        TMP_Text tmpText = GetComponentInChildren<TMP_Text>();
        if (isMusic)
        {
            tmpText.text = "Music: Off";
            isMusic = false;
        }
        else
        {
            tmpText.text = "Music: On";
            isMusic = true;
        }
    }

    private void OnClickBack()
    {
        UICtrlMainMenu.Instance.SelectLevel.gameObject.SetActive(true);
        UICtrlMainMenu.Instance.Settings.gameObject.SetActive(false);
    }
}

using __Data;
using __Data.Script;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMainMenu : GameBehaviour
{
    [SerializeField] protected Button btnMusic;
    [SerializeField] protected Button btnSfx;
    [SerializeField] protected Button btnBack;
    
    private bool _isMusic = true;
    private bool _isSfx = true;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnMusic();
        LoadBtnSfx();
        LoadBtnBack();
    }

    protected override void Start()
    {
        base.Start();
        _isMusic = PlayerPrefs.GetInt(PlayerPrefsString.MusicEnabled, 1) == 1;
        _isSfx = PlayerPrefs.GetInt(PlayerPrefsString.SFXEnabled, 1) == 1;
        UpdateUI();
        AudioManager.Instance.ApplyAudioSettings();
        
        btnMusic.onClick.AddListener(OnClickMusic);
        btnSfx.onClick.AddListener(OnClickSfx);
        btnBack.onClick.AddListener(OnClickBack);
    }
    
    private void LoadBtnMusic()
    {
        if (btnMusic != null) return;
        btnMusic = transform.Find("BtnMusic").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnMusic", gameObject);
    }
    
    private void LoadBtnSfx()
    {
        if (btnSfx != null) return;
        btnSfx = transform.Find("BtnSFX").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnSfx", gameObject);
    }

    private void LoadBtnBack()
    {
        if (btnBack != null) return;
        btnBack = transform.Find("BtnBack").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnBack", gameObject);
    }

    private void OnClickMusic()
    {
        _isMusic = !_isMusic;
        PlayerPrefs.SetInt(PlayerPrefsString.MusicEnabled, _isMusic ? 1 : 0);
        PlayerPrefs.Save();
        UpdateUI();
        AudioManager.Instance.ApplyAudioSettings();
    }
    
    private void OnClickSfx()
    {
        _isSfx = !_isSfx;
        PlayerPrefs.SetInt(PlayerPrefsString.SFXEnabled, _isSfx ? 1 : 0);
        PlayerPrefs.Save();
        UpdateUI();
        AudioManager.Instance.ApplyAudioSettings();
    }
    
    private void UpdateUI()
    {
        TMP_Text musicText = btnMusic.GetComponentInChildren<TMP_Text>();
        TMP_Text sfxText = btnSfx.GetComponentInChildren<TMP_Text>();
        musicText.text = _isMusic ? "Music: On" : "Music: Off";
        sfxText.text = _isSfx ? "SFX: On" : "SFX: Off";
    }

    private void OnClickBack()
    {
        UICtrlMainMenu.Instance.SelectLevel.gameObject.SetActive(true);
        UICtrlMainMenu.Instance.Settings.gameObject.SetActive(false);
    }
}

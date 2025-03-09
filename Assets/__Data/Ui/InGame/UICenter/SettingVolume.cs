using __Data;
using __Data.Script;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingVolume : GameBehaviour
{
    [SerializeField] protected Slider musicSlider;
    [SerializeField] protected Slider sfxSlider;
    [SerializeField] protected Button btnOke;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadMusicSlider();
        LoadSfxSlider();
        LoadBtnOke();
    }
    
    private void LoadMusicSlider()
    {
        if (musicSlider != null) return;
        musicSlider = transform.Find("Music").GetComponentInChildren<Slider>();
        Debug.LogWarning(transform.name + ": LoadMusicSlider", gameObject);
    }

    private void LoadSfxSlider()
    {
        if (sfxSlider != null) return;
        sfxSlider = transform.Find("SFX").GetComponentInChildren<Slider>();
        Debug.LogWarning(transform.name + ": LoadSFXSlider", gameObject);
    }

    private void LoadBtnOke()
    {
        if (btnOke != null) return;
        btnOke = transform.Find("BtnOk").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnOke", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        InitializeVolume();
        
        btnOke.onClick.AddListener(OnClickOke);
        musicSlider.onValueChanged.AddListener(UpdateMusicVolume);
        sfxSlider.onValueChanged.AddListener(UpdateSfxVolume);
    }

    private void InitializeVolume()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsString.MusicEnabled) == 1)
        {
            float musicVolume = PlayerPrefs.GetFloat(PlayerPrefsString.MusicVolume, 1f);
            musicSlider.value = musicVolume;
            UpdateMusicVolume(musicVolume);
        }
        else
            musicSlider.value = 0.001f;

        if (PlayerPrefs.GetInt(PlayerPrefsString.SFXEnabled) == 1)
        {
            float sfxVolume = PlayerPrefs.GetFloat(PlayerPrefsString.SfxVolume, 1f);
            sfxSlider.value = sfxVolume;
            UpdateSfxVolume(sfxVolume);
        }
        else
            sfxSlider.value = 0.001f;
    }
    
    private void UpdateMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(PlayerPrefsString.MusicVolume, volume);
        PlayerPrefs.SetInt(PlayerPrefsString.MusicEnabled, volume > 0.001f ? 1 : 0);
        PlayerPrefs.Save();
        AudioManager.Instance.ApplyAudioSettings();
    }
    
    private void UpdateSfxVolume(float volume)
    {
        PlayerPrefs.SetFloat(PlayerPrefsString.SfxVolume, volume);
        PlayerPrefs.SetInt(PlayerPrefsString.SFXEnabled, volume > 0.001f ? 1 : 0);
        PlayerPrefs.Save();
        AudioManager.Instance.ApplyAudioSettings();
    }
    
    private void OnClickOke()
    {
        UICenter.Instance.PauseMenu.SetActive(true);
        UICenter.Instance.Setting.SetActive(false);
    }
}

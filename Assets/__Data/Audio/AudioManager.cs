using System;
using System.Collections.Generic;
using __Data;
using __Data.Script;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : GameBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance => _instance;
    
    [SerializeField] protected AudioMixer audioMixer;
    
    [Header("Audio Source")]
    [SerializeField] protected AudioSource musicSource;
    public AudioSource MusicSource => musicSource;
    [SerializeField] protected AudioSource sfxSource;
    
    private readonly Dictionary<string, AudioSource> _loopSfxSources = new Dictionary<string, AudioSource>();

    [Header("Audio Clip")]
    [SerializeField] protected SfxData[] sfxDatas;

    [SerializeField] protected AudioClip[] listBackground;

    protected override void Awake()
    {
        base.Awake();
        if (AudioManager._instance != null) Debug.LogError("Only 1 AudioManager allow to exist");
        AudioManager._instance = this;
    }
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAudioMixer();
        LoadMusicSource();
        LoadSfxSource();
    }
    
    private void LoadAudioMixer()
    {
        if (audioMixer != null) return;
        audioMixer = Resources.Load<AudioMixer>("AudioMixer");
        Debug.Log(transform.name + ": LoadAudioMixer", gameObject);
    }
    
    private void LoadMusicSource()
    {
        if (musicSource != null) return;
        musicSource = transform.Find("MusicBackground")?.GetComponent<AudioSource>();
        Debug.Log(transform.name + ": LoadMusicSource", gameObject);
    }

    private void LoadSfxSource()
    {
        if (sfxSource != null) return;
        sfxSource = transform.Find("SoundEffect")?.GetComponent<AudioSource>();
        Debug.Log(transform.name + ": LoadSFXSource", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        ApplyAudioSettings();
        if (musicSource == null && listBackground.Length == 0) return;
        musicSource.clip = listBackground[Random.Range(0, listBackground.Length)];
        musicSource.Play();
    }

    public void ApplyAudioSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat(PlayerPrefsString.MusicVolume, 0.5f);
        bool musicEnabled = PlayerPrefs.GetInt(PlayerPrefsString.MusicEnabled, 1) == 1;
        float sfxVolume = PlayerPrefs.GetFloat(PlayerPrefsString.SfxVolume, 0.5f);
        bool sfxEnabled = PlayerPrefs.GetInt(PlayerPrefsString.SFXEnabled, 1) == 1;
        
        audioMixer.SetFloat("Music", musicEnabled ? ConvertToDecibel(musicVolume) : -80f);
        audioMixer.SetFloat("SFX", sfxEnabled ? ConvertToDecibel(sfxVolume) : -80f);
    }
    
    private float ConvertToDecibel(float volume) => Mathf.Log10(Mathf.Max(volume, 0.001f)) * 20;

    public void PlaySfx(string sfxName)
    {
        SfxData sfxData = Array.Find(sfxDatas, sfx => sfx.nameSfx == sfxName);
        if (sfxData != null && sfxData.clipSfx != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(sfxData.clipSfx);
        }
    }

    public void PlayLoopSfx(string sfxName)
    {
        if (_loopSfxSources.ContainsKey(sfxName)) return;

        SfxData sfxData = Array.Find(sfxDatas, sfx => sfx.nameSfx == sfxName);
        if (sfxData != null && sfxData.clipSfx != null)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = sfxData.clipSfx;
            newSource.loop = true;
            newSource.volume = 0.5f;
            newSource.Play();

            _loopSfxSources[sfxName] = newSource;
        }
    }

    public void StopLoopSfx(string sfxName)
    {
        if (_loopSfxSources.ContainsKey(sfxName))
        {
            Destroy(_loopSfxSources[sfxName]);
            _loopSfxSources.Remove(sfxName);
        }
    }
    
    [Serializable]
    public class SfxData
    {
        public string nameSfx;
        public AudioClip clipSfx;
    }
}

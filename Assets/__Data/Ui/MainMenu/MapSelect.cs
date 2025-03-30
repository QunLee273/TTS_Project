using __Data;
using __Data.Script;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelect : GameBehaviour
{
    [SerializeField] private Transform contentPanel;
    [SerializeField] protected Button[] levelButtons;
    [SerializeField] protected Image imageMap;
    [SerializeField] protected Button btnPlay;
    [SerializeField] protected Sprite[] levelImages;

    private int _playCountAds;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadContent();
        LoadImageMap();
        LoadBtnPlay();
    }

    protected override void Start()
    {
        base.Start();
        LoadButtons();
    }

    private void LoadContent()
    {
        if (contentPanel != null) return;
        Transform viewport = transform.Find("MapScroll/Viewport");
        if (viewport != null)
            contentPanel = viewport.Find("Content");
        Debug.LogWarning(transform.name + ": LoadContent", gameObject);
    }
    
    private void LoadImageMap()
    {
        if (imageMap != null) return;
        imageMap = transform.Find("Image/ImageMap").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadImageMap", gameObject);
    }
    
    private void LoadBtnPlay()
    {
        if (btnPlay != null) return;
        btnPlay = transform.Find("BtnPlay").GetComponent<Button>();
        btnPlay.gameObject.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadBtnPlay", gameObject);
    }

    private void LoadButtons()
    {
        levelButtons = contentPanel.GetComponentsInChildren<Button>();

        int unlockedLevel = PlayerPrefs.GetInt(PlayerPrefsString.UnlockedLevel, 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;
            
            levelButtons[i].onClick.RemoveAllListeners();
            levelButtons[i].onClick.AddListener(() => SelectLevel(levelIndex));
            
            bool isUnlocked = levelIndex <= unlockedLevel;
            levelButtons[i].interactable = isUnlocked;

            Transform lockIcon = levelButtons[i].transform.Find("LockIcon");
            Transform levelText = levelButtons[i].transform.Find("Text (TMP)");

            if (lockIcon != null)
                lockIcon.gameObject.SetActive(!isUnlocked);

            if (levelText != null)
                levelText.gameObject.SetActive(isUnlocked);
        }
    }

    private void SelectLevel(int levelIndex)
    {
        btnPlay.gameObject.SetActive(true); 
        btnPlay.onClick.AddListener(() => OnPlayButtonClick(levelIndex));
        if (imageMap != null && levelIndex - 1 < levelImages.Length)
        {
            imageMap.sprite = levelImages[levelIndex - 1];
        }
    }
    
    private void OnPlayButtonClick(int levelIndex)
    {
        _playCountAds = PlayerPrefs.GetInt(PlayerPrefsString.PlayCountAds, 0);
        _playCountAds++;

        if (_playCountAds >= 3)
        {
            _playCountAds = 0;

            System.Action onAdComplete = null;
            onAdComplete = () =>
            {
                LoadLevel(levelIndex);
                AdsManager.Instance.interstitialAds.OnAdCompleted -= onAdComplete;
            };

            AdsManager.Instance.interstitialAds.OnAdCompleted += onAdComplete;
            AdsManager.Instance.interstitialAds.ShowInterstitialAd();
        }
        else
        {
            LoadLevel(levelIndex);
        }

        PlayerPrefs.SetInt(PlayerPrefsString.PlayCountAds, _playCountAds);
        PlayerPrefs.Save();
    }

    private void LoadLevel(int levelIndex)
    {
        LoadingData.SelectedLevel = levelIndex;
        SceneManager.LoadScene("LoadingScene");
    }
}

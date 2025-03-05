using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    [SerializeField] protected string androidAdUnityId;
    [SerializeField] protected string iosAdUnityId;
    
    private string adUnitId;

    private void Awake()
    {
        #if UNITY_IOS
            adUnitId = iosAdUnityId;
        #elif UNITY_ANDROID
            adUnitId = androidAdUnityId;
        #endif
        
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    public void LoadBannerAd()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = BannerLoaded,
            errorCallback = BannerLoadedError
        };
        
        Advertisement.Banner.Load(adUnitId, options);
    }
    
    public void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions
        {
            showCallback = BannerShown,
            clickCallback = BannerClicked,
            hideCallback = BannerHidden
        };
        
        Advertisement.Banner.Show(adUnitId, options);
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    #region LoadCallBack
    private void BannerLoadedError(string message) { }

    private void BannerLoaded()
    {
        Debug.Log("Banner Ad Loaded");
    }
    #endregion
    
    #region ShowCallBack
    private void BannerHidden() { }

    private void BannerClicked() { }

    private void BannerShown() { }
    #endregion
}

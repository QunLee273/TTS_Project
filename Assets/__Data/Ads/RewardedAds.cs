using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] protected string androidAdUnityId;
    [SerializeField] protected string iosAdUnityId;
    
    private string adUnitId;
    public System.Action OnAdCompleted;

    private void Awake()
    {
        #if UNITY_IOS
            adUnitId = iosAdUnityId;
        #elif UNITY_ANDROID
            adUnitId = androidAdUnityId;
        #endif
    }
    
    public void LoadRewardedAd()
    {
        Advertisement.Load(adUnitId, this);
    }
    
    #region LoadCallBack
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Interstitial Ad Loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }
    #endregion

    public void ShowRewardedAd()
    {
        Advertisement.Show(adUnitId, this);
        LoadRewardedAd();
    }

    #region ShowCallBack
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == adUnitId && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Ads Fully Watched...."); // Will handle it later
            OnAdCompleted?.Invoke();
        }
    }
    #endregion
}

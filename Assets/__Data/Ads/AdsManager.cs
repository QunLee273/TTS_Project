using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Serialization;

public class AdsManager : MonoBehaviour
{
    public InitializeAds initializeAds;
    public BannerAds bannerAds;
    public InterstitialAds interstitialAds;
    public RewardedAds rewardedAds;
    
    public static AdsManager Instance { get; private set; }

    private void Awake()
    {
        if (AdsManager.Instance != null) Debug.LogError("Only 1 AdsManager allow to exist");
        AdsManager.Instance = this;
        
        bannerAds.LoadBannerAd();
        interstitialAds.LoadInterstitialAd();
        rewardedAds.LoadRewardedAd();
    }
}

using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] protected string androidGameId;
    [SerializeField] protected string iosGameId;
    [SerializeField] protected bool isTesting;
    
    private string gameId;

    private void Awake()
    {
        #if UNITY_IOS
            gameId = iosGameId;
        #elif UNITY_ANDROID
            gameId = androidGameId;
        #elif UNITY_EDITOR
            gameId = androidGameId;
        #endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, isTesting, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Ads initialized...");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message) { }
}

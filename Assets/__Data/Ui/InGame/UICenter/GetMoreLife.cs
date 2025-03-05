using __Data;
using __Data.Script;
using UnityEngine;
using UnityEngine.UI;

public class GetMoreLife : GameBehaviour
{
    [SerializeField] protected Button btnDecline;
    [SerializeField] protected Button btnCoin;
    [SerializeField] protected Button btnAds;
    [SerializeField] protected int reviveCost = 3000;
    [SerializeField] protected PlayerReceiver playerReceiver;

    private int _playerCoin;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnDecline();
        LoadBtnCoin();
        LoadBtnAds();
    }

    private void LoadBtnDecline()
    {
        if (btnDecline != null) return;
        btnDecline = GameObject.Find("BtnDeclineRevive").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnDecline", gameObject);
    }

    private void LoadBtnCoin()
    {
        if (btnCoin != null) return;
        btnCoin = GameObject.Find("BtnReviveCoin").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnCoin", gameObject);
    }

    private void LoadBtnAds()
    {
        if (btnAds != null) return;
        btnAds = GameObject.Find("BtnWatchAds").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnAds", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        btnDecline.onClick.AddListener(DeclineRevive);
        btnCoin.onClick.AddListener(AcceptReviveCoin);
        btnAds.onClick.AddListener(AcceptReviveAds);
    }

    private void AcceptReviveCoin()
    {
        _playerCoin = PlayerPrefs.GetInt(PlayerPrefsString.AmountCoins);
        if (_playerCoin >= reviveCost)
        {
            _playerCoin -= reviveCost;
            PlayerPrefs.SetInt(PlayerPrefsString.AmountCoins, _playerCoin);
            PlayerRevive();
        }
        else
        {
            Debug.Log("Không đủ vàng!");
        }
    }

    private void AcceptReviveAds()
    {
        if (AdsManager.Instance.rewardedAds == null) return;
        
        AdsManager.Instance.rewardedAds.OnAdCompleted += PlayerRevive;
        AdsManager.Instance.rewardedAds.ShowRewardedAd();
    }

    private void PlayerRevive()
    {
        playerReceiver.HasUsedRevive = true;
        playerReceiver.Dead = false;
        playerReceiver.Add(3);
        PlayerController.Instance.Respawn();
        UICenter.Instance.GetMoreLife.SetActive(false);
        Time.timeScale = 1;
    }

    private void DeclineRevive()
    {
        UICenter.Instance.YouDead.SetActive(true);
        UICenter.Instance.GetMoreLife.SetActive(false);
    }
}

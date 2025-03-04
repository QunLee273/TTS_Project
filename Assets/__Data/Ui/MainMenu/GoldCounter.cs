using __Data;
using __Data.Script;
using TMPro;
using UnityEngine;

public class GoldCounter : GameBehaviour
{
    [SerializeField] protected TMP_Text amountCoinText;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTextAmount();
    }

    private void LoadTextAmount()
    {
        if (amountCoinText != null) return;
        amountCoinText = GetComponentInChildren<TMP_Text>();
        Debug.LogWarning(transform.name + ": LoadImageMap", gameObject);
    }

    protected void Update()
    {
        LoadCoinAmount();
    }

    private void LoadCoinAmount()
    {
        int coinAmount = PlayerPrefs.GetInt(PlayerPrefsString.AmountCoins, 0);
        
        amountCoinText.text = coinAmount.ToString();
    }
}

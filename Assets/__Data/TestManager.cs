using System;
using __Data.Script;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public bool pause;
    public bool addCoin;
    public bool resetPlayerPref;

    private void Start()
    {
        ResetPlayerPref();
        AddCoinPlayerPref();
    }

    private void Update()
    {
        PauseGame();
    }

    private void ResetPlayerPref()
    {
        if (!resetPlayerPref) return;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    
    private void AddCoinPlayerPref()
    {
        if (!addCoin) return;
        PlayerPrefs.SetInt(PlayerPrefsString.AmountCoins, 999999999);
        PlayerPrefs.Save();
    }

    private void PauseGame()
    {
        if (!Input.GetKeyDown(KeyCode.Q)) return;
        if (!pause)
        {
            pause = true;
            Time.timeScale = 0;
        }
        else
        {
            pause = false;
            Time.timeScale = 1;
        }
    }
}

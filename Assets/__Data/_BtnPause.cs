using System;
using __Data.Script;
using UnityEngine;

public class _BtnPause : MonoBehaviour
{
    public bool pause;

    private void Start()
    {
        PlayerPrefs.SetInt(PlayerPrefsString.UnlockedLevel, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
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
}

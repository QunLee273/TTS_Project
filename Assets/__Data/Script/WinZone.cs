using System.Collections;
using __Data;
using __Data.Script;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : GameBehaviour
{
    [SerializeField] protected int numberUnlock;
    [SerializeField] protected int mapIndex;

    protected override void Start()
    {
        base.Start();
        numberUnlock = PlayerPrefs.GetInt(PlayerPrefsString.UnlockedLevel, 1);
        mapIndex = SceneManager.GetActiveScene().buildIndex;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            if (numberUnlock > mapIndex)
                UICenter.Instance.CompleteMap.SetActive(true);

            if (numberUnlock == mapIndex)
            {
                PlayerPrefs.SetInt(PlayerPrefsString.UnlockedLevel, numberUnlock + 1);
                UICenter.Instance.CompleteMap.SetActive(true);
            }
        }
    }
}
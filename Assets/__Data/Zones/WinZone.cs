using System.Collections;
using __Data;
using __Data.Script;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : GameBehaviour
{
    [SerializeField] protected int numberUnlock;
    [SerializeField] protected int mapIndex;
    [SerializeField] protected GameObject target;

    private bool _winZone;
    protected override void Start()
    {
        base.Start();
        numberUnlock = PlayerPrefs.GetInt(PlayerPrefsString.UnlockedLevel, 1);
        mapIndex = SceneManager.GetActiveScene().buildIndex;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_winZone)
        {
            _winZone = true;
            UIandAudio();
            StartCoroutine(MovePlayerAndWin(other));
        }
    }

    private IEnumerator MovePlayerAndWin(Collider2D playerCollider)
    {
        var moveScript = playerCollider.GetComponentInChildren<PlayerMove>();
        if (moveScript != null) moveScript.enabled = false;
        
        Animator animator = playerCollider.transform.Find("Model").GetComponent<Animator>();
        if (animator != null)
            animator.SetBool(AnimString.isMove, true);
        
        Transform player = playerCollider.transform;
        Vector3 startPos = player.position;
        Vector3 targetPos = target.transform.position;

        float duration = 2.5f, elapsed = 0f;
        while (elapsed < duration)
        {
            player.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        player.position = targetPos;
        
        animator.SetBool(AnimString.isMove, false);
        
        WinGame();
    }

    private void WinGame()
    {
        if (numberUnlock > mapIndex)
            UICenter.Instance.CompleteMap.SetActive(true);

        if (numberUnlock == mapIndex)
        {
            PlayerPrefs.SetInt(PlayerPrefsString.UnlockedLevel, numberUnlock + 1);
            UICenter.Instance.CompleteMap.SetActive(true);
        }
        Time.timeScale = 0;
    }

    private void UIandAudio()
    {
        UIPlayGame.Instance.gameObject.SetActive(false);
        AudioManager.Instance.PlaySfx("WinGame");
        AudioManager.Instance.MusicSource.clip = null;
    }
}
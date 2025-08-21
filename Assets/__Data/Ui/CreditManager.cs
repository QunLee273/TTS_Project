using System.Collections;
using __Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditManager : GameBehaviour
{
    [SerializeField] protected Button btnSkip;
    [SerializeField] protected GameObject creditPanel;
    [SerializeField] protected GameObject creditText;
    [SerializeField] protected float scrollSpeed = 50f;
    
    [SerializeField] protected AudioClip creditSound;

    private RectTransform _creditRect;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBtnSkip();
        LoadPanel();
        LoadText();
    }

    private void LoadBtnSkip()
    {
        if (btnSkip != null) return;
        btnSkip = GameObject.Find("BtnSkip").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnSkip", gameObject);
    }

    private void LoadPanel()
    {
        if (creditPanel != null) return;
        creditPanel = GameObject.Find("CreditPanel");
        Debug.LogWarning(transform.name + ": LoadPanel", gameObject);
    }

    private void LoadText()
    {
        if (creditText != null) return;
        creditText = GameObject.Find("CreditText");
        Debug.LogWarning(transform.name + ": LoadText", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        btnSkip.onClick.AddListener(SkipCredit);
        _creditRect = creditText.GetComponent<RectTransform>();
        creditPanel.SetActive(false);
        ShowCredit();
    }

    private void ShowCredit()
    {
        AudioManager.Instance.PlayMusic(creditSound);
        creditPanel.SetActive(true);
        StartCoroutine(ScrollCredit());
    }

    IEnumerator ScrollCredit()
    {
        float startY = _creditRect.anchoredPosition.y;
        float endY = startY + 3000f;

        while (_creditRect.anchoredPosition.y < endY)
        {
            _creditRect.anchoredPosition += Vector2.up * (scrollSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        LoadingData.SelectedLevel = 0;
        SceneManager.LoadScene("LoadingScene");
    }

    private void SkipCredit()
    {
        LoadingData.SelectedLevel = 0;
        SceneManager.LoadScene("LoadingScene");
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    public GameObject creditPanel; // Panel chứa credit
    public GameObject creditText; // Text hiển thị credit
    public float scrollSpeed = 50f; // Tốc độ chạy chữ

    private RectTransform creditRect;
    
    void Start()
    {
        creditRect = creditText.GetComponent<RectTransform>();
        creditPanel.SetActive(false);
        ShowCredit();
    }

    public void ShowCredit()
    {
        creditPanel.SetActive(true);
        StartCoroutine(ScrollCredit());
    }

    IEnumerator ScrollCredit()
    {
        float startY = creditRect.anchoredPosition.y;
        float endY = startY + 2000f; // Điều chỉnh giá trị này tùy theo độ dài credit

        while (creditRect.anchoredPosition.y < endY)
        {
            creditRect.anchoredPosition += Vector2.up * (scrollSpeed * Time.deltaTime);
            yield return null;
        }

        // Sau khi credit chạy xong, trở về màn hình chính
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }

    public void SkipCredit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

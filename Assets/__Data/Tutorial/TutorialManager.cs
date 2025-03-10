using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("UI Tutorial Elements")]
    public GameObject tutorialPanel;
    public TMP_Text textTutorial;
    public GameObject arrowIndicator;
    public Image image;

    [Header("Button Targets")]
    public RectTransform[] tutorialButtons;
    public Sprite[] tutorialImages;
    public string[] tutorialMessages;
    
    public Sprite[] imageButtons;
    private Coroutine _arrowMoveCoroutine;

    protected void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            tutorialPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 275);
            arrowIndicator.GetComponent<RectTransform>().anchoredPosition = new Vector2(150, 0);
            Time.timeScale = 1;
            if (_arrowMoveCoroutine != null) StopCoroutine(_arrowMoveCoroutine);
        }
            
    }
    
    public void ShowTutorialStep(int step)
    {
        tutorialPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -275);
        Time.timeScale = 0f;

        if (step < 0 || step >= tutorialMessages.Length) return;

        ShowStep(tutorialMessages[step], imageButtons[step] , tutorialButtons[step]);
    }

    private void ShowStep(string message, Sprite sprite, RectTransform target)
    {
        textTutorial.text = message;
        if (sprite != null)
        {
            image.gameObject.SetActive(true);
            image.sprite = sprite;
        }
        else
            image.gameObject.SetActive(false);
        
        if (target == null) return;

        RectTransform arrowRect = arrowIndicator.GetComponent<RectTransform>();
        arrowRect.anchoredPosition = target.anchoredPosition + new Vector2(0, 150);

        if (_arrowMoveCoroutine != null) StopCoroutine(_arrowMoveCoroutine);
        _arrowMoveCoroutine = StartCoroutine(MoveArrow(arrowRect));
    }

    private IEnumerator MoveArrow(RectTransform arrow)
    {
        Vector2 startPos = arrow.anchoredPosition;
        Vector2 targetPos = startPos + new Vector2(0, 20);

        while (true)
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.unscaledDeltaTime * 2;
                arrow.anchoredPosition = Vector2.Lerp(startPos, targetPos, Mathf.PingPong(t, 1));
                yield return null;
            }
        }
    }
}

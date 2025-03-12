using System.Collections;
using UnityEngine;

public class TutorialManager : UiTutorial
{
    [Header("Show Tutorial Elements")]
    [SerializeField] protected RectTransform[] tutorialButtons;
    [SerializeField] protected Sprite[] tutorialImages;
    [SerializeField] protected string[] tutorialMessages;
    [SerializeField] protected Sprite[] imageButtons;
    
    private Coroutine _arrowMoveCoroutine;
    private bool _isArrowMoving;

    protected override void Start()
    {
        base.Start();
        btnSkip.onClick.AddListener(OnClickSkip);
    }

    public void ShowTutorialStep(int step)
    {
        _isArrowMoving = true;
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

        while (_isArrowMoving)
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.unscaledDeltaTime * 2;
                arrow.anchoredPosition = Vector2.Lerp(startPos, targetPos, Mathf.PingPong(t, 1));
                yield return null;
            }
        }
        arrowIndicator.GetComponent<RectTransform>().anchoredPosition = new Vector2(150, 0);
    }

    private void OnClickSkip()
    {
        tutorialPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 275);
        Time.timeScale = 1;
        _isArrowMoving = false;
    }
}

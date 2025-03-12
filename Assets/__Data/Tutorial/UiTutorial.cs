using __Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiTutorial : GameBehaviour
{
    [Header("UI Tutorial Elements")]
    [SerializeField] protected GameObject tutorialPanel;
    [SerializeField] protected TMP_Text textTutorial;
    [SerializeField] protected GameObject arrowIndicator;
    [SerializeField] protected Image image;
    [SerializeField] protected Button btnSkip;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTutorialPanel();
        LoadTxtTutorial();
        LoadImageTutorial();
        LoadBtnSkip();
    }

    private void LoadTutorialPanel()
    {
        if (tutorialPanel != null) return;
        tutorialPanel = transform.Find("TutorialPanel").gameObject;
        Debug.LogWarning(transform.name + ": LoadTutorialPanel", gameObject);
    }

    private void LoadTxtTutorial()
    {
        if (textTutorial != null) return;
        textTutorial = transform.Find("TutorialText").GetComponent<TMP_Text>();
        Debug.LogWarning(transform.name + ": LoadTxtTutorial", gameObject);
    }

    private void LoadImageTutorial()
    {
        if (image != null) return;
        image = transform.Find("Image").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadImageTutorial", gameObject);
    }

    private void LoadBtnSkip()
    {
        if (btnSkip != null) return;
        btnSkip = transform.Find("BtnSkip").GetComponent<Button>();
        Debug.LogWarning(transform.name + ": LoadBtnSkip", gameObject);
    }
}

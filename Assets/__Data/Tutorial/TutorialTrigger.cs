using __Data;
using __Data.Script;
using UnityEngine;

public class TutorialTrigger : GameBehaviour
{
    [SerializeField] protected TutorialManager tutorialManager;
    [SerializeField] protected int tutorialStep;

    protected override void Start()
    {
        base.Start();
        if (PlayerPrefs.GetInt(PlayerPrefsString.UnlockedLevel) == 1)
        {
            UIBottomRight.Instance.BtnJump.gameObject.SetActive(false);
            UIBottomRight.Instance.BtnAttack.gameObject.SetActive(false);
            UIBottomRight.Instance.BtnDash.gameObject.SetActive(false);
            UIBottomRight.Instance.BtnInvisible.gameObject.SetActive(false);
        }
        else Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (tutorialStep == 0) UIBottomRight.Instance.BtnJump.gameObject.SetActive(true);
            if (tutorialStep == 1) UIBottomRight.Instance.BtnAttack.gameObject.SetActive(true);
            if (tutorialStep == 2) UIBottomRight.Instance.BtnDash.gameObject.SetActive(true);
            if (tutorialStep == 3) UIBottomRight.Instance.BtnInvisible.gameObject.SetActive(true);
            tutorialManager.ShowTutorialStep(tutorialStep);
            Destroy(gameObject);
        }
    }
}

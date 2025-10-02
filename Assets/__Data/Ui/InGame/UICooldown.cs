using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICooldown : MonoBehaviour
{
    [Header("Core References")]
    [SerializeField] private Button button;           // Optional: nút gốc
    [SerializeField] private Image cooldownOverlay;   // Ảnh overlay

    [Header("Active Duration UI (Optional)")]
    [SerializeField] private Scrollbar activeBar;     // Thanh active 
    [SerializeField] private bool useActiveBar = false;

    private Coroutine _cooldownRoutine;
    private Coroutine _activeRoutine;

    private void Awake()
    {
        if (button == null) button = GetComponent<Button>();

        if (cooldownOverlay != null)
        {
            cooldownOverlay.type = Image.Type.Filled;
            cooldownOverlay.fillMethod = Image.FillMethod.Radial360;
            cooldownOverlay.fillAmount = 0f;
        }

        if (useActiveBar && activeBar != null)
        {
            activeBar.size = 0f;
            activeBar.gameObject.SetActive(false);
        }
    }
    
    public void StartCooldown(float time)
    {
        if (_cooldownRoutine != null) StopCoroutine(_cooldownRoutine);
        _cooldownRoutine = StartCoroutine(CooldownRoutine(time));
    }
    
    public void StartActive(float time)
    {
        if (!useActiveBar || activeBar == null) return;
        if (_activeRoutine != null) StopCoroutine(_activeRoutine);
        _activeRoutine = StartCoroutine(ActiveRoutine(time));
    }
    
    public void StopActive()
    {
        if (_activeRoutine != null) StopCoroutine(_activeRoutine);
        _activeRoutine = null;

        if (useActiveBar && activeBar != null)
        {
            activeBar.size = 0f;
            activeBar.gameObject.SetActive(false);
        }
    }

    private IEnumerator CooldownRoutine(float cooldownTime)
    {
        if (button != null) button.interactable = false;
        cooldownOverlay.fillAmount = 1f;

        float t = cooldownTime;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            cooldownOverlay.fillAmount = t / cooldownTime;
            yield return null;
        }

        cooldownOverlay.fillAmount = 0f;
        if (button != null) button.interactable = true;
        _cooldownRoutine = null;
    }

    private IEnumerator ActiveRoutine(float activeTime)
    {
        activeBar.gameObject.SetActive(true);
        float t = activeTime;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            activeBar.size = t / activeTime;
            yield return null;
        }

        activeBar.size = 0f;
        activeBar.gameObject.SetActive(false);
        _activeRoutine = null;
    }
}

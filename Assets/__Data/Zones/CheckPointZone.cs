using System.Collections;
using __Data;
using __Data.Script;
using UnityEngine;

public class CheckPointZone : GameBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected float duration = 2f;
    
    private bool _isCheck;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAnimator();
        LoadRenderer();
    }

    private void LoadAnimator()
    {
        if (animator != null) return;
        animator = GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    private void LoadRenderer()
    {
        if (spriteRenderer != null) return;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.LogWarning(transform.name + ": LoadRenderer", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_isCheck)
        {
            _isCheck = true;
            animator.SetBool(AnimString.check, true);
            StartCoroutine(BlackToWhiteRoutine());
        }
    }
    
    private IEnumerator BlackToWhiteRoutine()
    {
        float elapsedTime = 0f;
        Color startColor = Color.black;
        Color targetColor = Color.white;

        while (elapsedTime < duration)
        {
            spriteRenderer.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = targetColor;
    }
}

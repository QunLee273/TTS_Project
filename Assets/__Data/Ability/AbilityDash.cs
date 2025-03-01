using System.Collections;
using __Data.Script;
using UnityEngine;
using UnityEngine.Serialization;

public class AbilityDash : AbilityAttack
{
    [FormerlySerializedAs("dashSpeed")]
    [Header("Ability Dash")]
    [SerializeField] protected float dashPower = 5f;
    [SerializeField] protected float dashDuration = 0.2f;
    [SerializeField] protected float dashCooldown = 1f;
    [SerializeField] protected SpriteRenderer dashRenderer;
    public SpriteRenderer DashRenderer => dashRenderer;
    
    private bool _isDashing = false;
    private float _lastDashTime;
    private float _originalGravity;
    private Vector3 _dashDirection;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpriteRenderer();
    }

    protected override void Start()
    {
        base.Start();
        _originalGravity = rb.gravityScale;
    }

    private void LoadSpriteRenderer()
    {
        if (dashRenderer != null) return;
        GameObject obj = transform.parent.parent.gameObject;
        dashRenderer = obj.GetComponentInChildren<SpriteRenderer>();
        Debug.LogWarning(transform.name + ": LoadSpriteRenderer", gameObject);
    }

    public void OnClickDash()
    {
        if (Time.time > _lastDashTime + dashCooldown && !_isDashing)
            StartDash();
        if (_isDashing)
            ContinueDash();
    }

    private void StartDash()
    {
        _lastDashTime = Time.time;
        _isDashing = true;
        rb.gravityScale = 0f;
        animator.SetBool(AnimString.isDash, true);
        animator.SetBool(AnimString.canMove, false);
        _dashDirection = new Vector3(Mathf.Sign(transform.parent.parent.localScale.x), 0f, 0f);
        StartCoroutine(CreateGhostTrail());
        Invoke(nameof(EndDash), dashDuration);
    }

    private void ContinueDash()
    {
        rb.linearVelocity = _dashDirection * dashPower;
    }

    private void EndDash()
    {
        _isDashing = false;
        rb.gravityScale = _originalGravity;
        animator.SetBool(AnimString.isDash, false);
        animator.SetBool(AnimString.canMove, true);
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    IEnumerator CreateGhostTrail()
    {
        while (_isDashing)
        {
            GameObject ghost = new GameObject("Ghost");
            SpriteRenderer ghostSr = ghost.AddComponent<SpriteRenderer>();
            ghostSr.sprite = dashRenderer.sprite;
            ghostSr.flipX = dashRenderer.flipX;
            ghostSr.sortingLayerName = "Player";
            ghost.transform.position = transform.parent.parent.position;
            ghost.transform.localScale = transform.parent.parent.localScale;
            ghostSr.color = new Color(1f, 1f, 1f, 0.5f);
            Destroy(ghost, 0.3f);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    public void DashAttack()
    {
        foreach (Collider2D enemy in detectedAttack.ToArray())
        {
            EnemyCtrl enemyCtrl = enemy.GetComponentInChildren<EnemyCtrl>();
            if (enemyCtrl != null)
                enemyCtrl.DamageReceiver.Deduct(enemyCtrl.DamageReceiver.Lifes);
            
            BreakablesCtrl breakablesCtrl = enemy.GetComponentInChildren<BreakablesCtrl>();
            if (breakablesCtrl != null)
                breakablesCtrl.DamageReceiver.Deduct(breakablesCtrl.DamageReceiver.Lifes);
            
            BossCtrl bossCtrl = enemy.GetComponentInChildren<BossCtrl>();
            if (bossCtrl != null)
                bossCtrl.DamageReceiver.Deduct(1);
        }
    }
}

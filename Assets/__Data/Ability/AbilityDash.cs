using System.Collections;
using __Data.Script;
using UnityEngine;

public class AbilityDash : AbilityAttack
{
    [Header("Ability Dash")]
    [SerializeField] protected float dashPower = 10f;
    [SerializeField] protected float dashDuration = 1f;
    [SerializeField] protected float dashCooldownBase = 12f;
    [SerializeField] protected int dashLevel;
    [SerializeField] protected SpriteRenderer dashRenderer;
    
    private bool _isDashing = false;
    private float _lastDashTime;
    private float _originalGravity;
    private Vector3 _dashDirection;
    private float _dashCooldown;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpriteRenderer();
    }

    protected override void Start()
    {
        base.Start();
        _originalGravity = rb.gravityScale;
        UpdateDashCooldown();
        _lastDashTime = -_dashCooldown;
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
        if (Time.time > _lastDashTime + _dashCooldown && !_isDashing)
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
        
        UIBottomRight.Instance.BtnDash.GetComponent<UICooldown>().StartCooldown(_dashCooldown);
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
            AudioManager.Instance.PlaySfx("Dash");
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

    private void UpdateDashCooldown()
    {
        dashLevel = PlayerPrefs.GetInt(PlayerPrefsString.SkillLevel_ + 0);

        _dashCooldown = dashCooldownBase;

        for (int i = 0; i <= dashLevel; i++)
        {
            if (i is >= 1 and <= 4)
                _dashCooldown -= 1;
            else if (i == 5)
                _dashCooldown -= 2;
        }
        
    }
}

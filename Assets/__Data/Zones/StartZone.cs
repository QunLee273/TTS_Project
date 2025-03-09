using System.Collections;
using __Data;
using __Data.Script;
using UnityEngine;

public class StartZone : GameBehaviour
{
    [SerializeField] private float checkRadius = 1f;
    [SerializeField] private LayerMask playerLayer;
    private bool triggered = false;

    protected override void Start()
    {
        UIPlayGame.Instance.gameObject.SetActive(false);
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, checkRadius, playerLayer);
        if (playerCollider != null && !triggered)
        {
            triggered = true;
            StartCoroutine(MovePlayerToStart(playerCollider));
        }
    }
    
    private IEnumerator MovePlayerToStart(Collider2D playerCollider)
    {
        yield return new WaitForSeconds(0.1f);
        var moveScript = playerCollider.GetComponentInChildren<PlayerMove>();
        if (moveScript != null) moveScript.enabled = false;
        
        Animator animator = playerCollider.transform.Find("Model").GetComponent<Animator>();
        if (animator != null)
            animator.SetBool(AnimString.isMove, true);
        
        Transform player = playerCollider.transform;
        Vector3 startPos = player.position;
        Vector3 targetPos = new Vector3(0, player.position.y, 0);

        float duration = 2.5f, elapsed = 0f;
        while (elapsed < duration)
        {
            player.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        player.position = targetPos;
        
        animator.SetBool(AnimString.isMove, false);
        moveScript.enabled = true;
        
        UIPlayGame.Instance.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}

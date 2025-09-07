using System;
using __Data;
using UnityEngine;

public class BackgroundController : GameBehaviour
{
    [SerializeField] protected GameObject cam;
    [SerializeField] protected SpriteRenderer[] bgs;
    
    [SerializeField] protected float bgSpeed = 0.01f;
    [SerializeField] protected PlayerMove player;

    private Vector3 _firstBgOffset;

    private void OnDrawGizmosSelected()
    {
        if (Camera.main != null) cam = Camera.main.gameObject;
        bgs = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < bgs.Length; i++)
        {
            bgs[i].sortingLayerName = "Background";
            bgs[i].sortingOrder = i;
        }
    }

    protected override void Start()
    {
        base.Start();
        foreach (var t in bgs)
            t.transform.position = new Vector3(cam.transform.position.x, t.transform.position.y, t.transform.position.z);

        if (bgs.Length > 0)
            _firstBgOffset = bgs[0].transform.position - cam.transform.position;
    }
    
    protected void Update()
    {
        float moveDir = GetPlayerMoveDirection();

        for (int i = 0; i < bgs.Length; i++)
        {
            MoveBackground(bgs[i], i, moveDir);
        }
    }

    private void MoveBackground(SpriteRenderer bg, int index, float moveDir)
    {
        if (index == 0) 
        {
            bg.transform.position = cam.transform.position + _firstBgOffset;
            return;
        }
        
        bg.transform.Translate(Vector3.right * (moveDir * (bgSpeed + index) * Time.deltaTime));

        float spriteWidth = bg.bounds.size.x;
        if (Mathf.Abs(cam.transform.position.x - bg.transform.position.x) >= spriteWidth)
        {
            bg.transform.position = new Vector3(
                cam.transform.position.x,
                bg.transform.position.y,
                bg.transform.position.z
            );
        }
    }
    
    private float GetPlayerMoveDirection()
    {
        if (player == null) return 0;

        float vx = player.Rb.linearVelocity.x;
        if (vx > 0.1f) return -0.5f;
        if (vx < -0.1f) return 0.5f;
        return 0;
    }
}

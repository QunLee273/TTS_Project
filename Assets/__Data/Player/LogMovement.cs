using UnityEngine;

public class LogMovement : ObjMovement
{
    [Header("Log Movement")]
    private bool _isMoveLeft, _isMoveRight;
    
    public void OnPointerDownLeft()  { _isMoveLeft = true; }
    public void OnPointerUpLeft()    { _isMoveLeft = false; }
    public void OnPointerDownRight() { _isMoveRight = true; }
    public void OnPointerUpRight()   { _isMoveRight = false; }
    private void Update()
    {
        if (_isMoveLeft || Input.GetAxis("Horizontal") < 0) HandleMovement(-1);
        else if (_isMoveRight || Input.GetAxis("Horizontal") > 0) HandleMovement(1);
        else HandleMovement(0);
    }
    
    private void HandleMovement(float direction )
    {
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    }
}

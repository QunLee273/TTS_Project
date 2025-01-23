using System;
using __Data.Script;
using UnityEngine;

public class PlayerController : ObjController
{
    [SerializeField] public static bool _isAlive = true;
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            ObjMovement.Animator.GetBool(AnimString.isAlive);
        } 
    }
    private void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.CompareTag("Trap"))
        {
            Debug.Log(collide.gameObject.name);
            ObjMovement.Animator.Play("Player_Death");
            _isAlive = false;
        }
    }
}

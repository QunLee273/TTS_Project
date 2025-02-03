using __Data.Script;
using UnityEngine;

public class PlayerAttack : AbilityAttack
{
    protected void Update()
    {
        OnAttack();
    }
    
    private void OnAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger(AnimString.attackTrigger);
            animator.Play("Player_Atk1");
        }
    }
}

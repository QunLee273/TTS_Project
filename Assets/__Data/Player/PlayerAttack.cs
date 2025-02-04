using __Data.Script;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttack : AbilityAttack
{
    protected void Update()
    {
        OnAttack();
    }
    
    private void OnAttack()
    {
        // animator.SetTrigger(AnimString.attackTrigger);
        // animator.Play("Player_Atk1");
    }
}

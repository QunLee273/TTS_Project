using System.Collections;
using UnityEngine;

public class PlayerReceiver : DamageReceiver
{
    protected override void Reborn()
    {
        base.Reborn();
        lifes = objController.GameObjectSo.life;
    }

    protected override void OnDead()
    {
        UICenter.Instance.YouDead.SetActive(true);
        StartCoroutine(PauseGameOnDead());
    }

    private IEnumerator PauseGameOnDead()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    } 
}

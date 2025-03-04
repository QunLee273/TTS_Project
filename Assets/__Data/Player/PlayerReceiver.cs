using System.Collections;
using __Data.Script;
using UnityEngine;

public class PlayerReceiver : DamageReceiver
{
    [Header("Player receiver")]
    [SerializeField] protected bool hasUsedRevive = false;
    public bool HasUsedRevive
    {
        get => hasUsedRevive;
        set => hasUsedRevive = value;
    }
    protected override void Reborn()
    {
        base.Reborn();
        int lifeSo = objController.GameObjectSo.life;

        int survivalLv = PlayerPrefs.GetInt(PlayerPrefsString.SkillLevel_ + 1);
        for (int i = 0; i <= survivalLv; i++)
        {
            if (i is 1 or 3) 
                lifeSo += 1;
            if (i == 5) 
                lifeSo += 2;
        }
        
        lifes = lifeSo;
    }

    protected override void OnDead()
    {
        if (!hasUsedRevive)
            UICenter.Instance.GetMoreLife.SetActive(true);
        else
            UICenter.Instance.YouDead.SetActive(true);
        StartCoroutine(PauseGameOnDead());
    }

    private IEnumerator PauseGameOnDead()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    } 
}

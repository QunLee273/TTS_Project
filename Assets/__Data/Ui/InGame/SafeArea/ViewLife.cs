using __Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewLife : GameBehaviour
{
    [SerializeField] protected Transform player;
    [SerializeField] protected TMP_Text lifeText;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayer();
        LoadText();
    }

    private void LoadPlayer()
    {
        if (player != null) return;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.LogWarning(transform.name + ": LoadPlayer", gameObject);
    }

    private void LoadText()
    {
        if (lifeText != null) return;
        lifeText = GetComponentInChildren<TMP_Text>();
        Debug.LogWarning(transform.name + ": LoadImageMap", gameObject);
    }

    protected void Update()
    {
        LoadLife();
    }

    private void LoadLife()
    {
        PlayerReceiver playerReceiver = player.GetComponentInChildren<PlayerReceiver>();

        int lifeAmount = playerReceiver.Lifes;
        
        lifeText.text = lifeAmount.ToString();
    }
}

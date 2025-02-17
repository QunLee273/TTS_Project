using __Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : GameBehaviour
{
    [SerializeField] protected int sceneIndex = 0;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            SceneManager.LoadScene(sceneIndex);
    }
}

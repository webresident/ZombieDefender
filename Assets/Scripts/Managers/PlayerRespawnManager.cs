using UnityEngine;

public class PlayerRespawnManager : MonoBehaviour
{
    public static PlayerRespawnManager instance;

    private bool isPlayerDead = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CharacterHealth.OnPlayerDeath += GetCurrentPlayerHealth;
    }

    private void GetCurrentPlayerHealth(bool state)
    {
        isPlayerDead = state;
    }

    private void OnDisable()
    {
        CharacterHealth.OnPlayerDeath -= GetCurrentPlayerHealth;
    }

    public bool IsPlayerDead()
    {
        return isPlayerDead;
    }
}

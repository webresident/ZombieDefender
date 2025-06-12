using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private bool isPlayerDead = false;

    public float attackTimer = 0f;
    public float hitTimer = 0f;
    public float rollTimer = 0f;

    public bool isNotInteractable = false;
    private float currentTimer = 0f;

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

    private void Update()
    {
        if (isNotInteractable)
        {
            if (currentTimer <= 0f)
            {
                isNotInteractable = false;
            }
            else
            {
                currentTimer -= Time.deltaTime;
            }
        }
    }

    private void GetCurrentPlayerHealth(bool state)
    {
        isPlayerDead = state;
    }

    public void PlayerRolling()
    {
        isNotInteractable = true;
        currentTimer = rollTimer;
    }

    public void SetAttack()
    {
        isNotInteractable = true;
        currentTimer = attackTimer;
    }

    public void GetHit()
    {
        isNotInteractable = true;
        currentTimer = hitTimer;
    }

    public bool IsPlayerDead()
    {
        return isPlayerDead;
    }
    private void OnDisable()
    {
        CharacterHealth.OnPlayerDeath -= GetCurrentPlayerHealth;
    }
}

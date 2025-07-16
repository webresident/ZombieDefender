using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private bool isPlayerDead = false;

    [Header("Combat and moving")]
    public float attackTimer = 0f;
    public float hitTimer = 0f;
    public float rollTimer = 0f;

    public bool isNotInteractable = false;

    private float currentTimer = 0f;

    [Header("Jumping")]
    public float jumpTimer = 0f;
    public bool isJumped = false;

    private float currentJumpTimer = 0f;

    // it's just example for npc questions
    public int LVL { get; set; }

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

        if (isJumped)
        {
            currentJumpTimer -= Time.deltaTime;
            
            if (currentJumpTimer <= 0f)
            {
                isJumped = false;
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

    public void SetJump()
    {
        isJumped = true;
        currentJumpTimer = jumpTimer;
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

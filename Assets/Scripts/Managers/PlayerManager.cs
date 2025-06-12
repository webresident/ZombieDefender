using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private bool isPlayerDead = false;

    private float rollTime = 0f;
    public bool isRolling = false;

    private float attackTime = 0f;
    public bool isAttack = false;

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
        if (isRolling)
        {
            if(rollTime <= 0f)
            {
                isRolling = false;
            }
            else
            {
                rollTime -= Time.deltaTime;
            }
        }

        if (isAttack)
        {
            if (attackTime <= 0f)
            {
                isAttack = false;
            }
            else
            {
                attackTime -= Time.deltaTime;
            }
        }
    }

    private void GetCurrentPlayerHealth(bool state)
    {
        isPlayerDead = state;
    }

    public void PlayerRolling()
    {
        isRolling = true;
        rollTime = 1f;
    }

    public void SetAttack()
    {
        isAttack = true;
        attackTime = 1f;
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

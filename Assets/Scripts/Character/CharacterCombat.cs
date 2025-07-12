using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    public static event Action<int> OnAttack;

    [SerializeField] private float timer = 1f;
    private float time = 0;

    private void Start()
    {
        time = timer;
    }
    private void Update()
    {
        if (PlayerManager.instance.IsPlayerDead())
        {
            return;
        }

        if (time <= 0)
        {
            LeftAttack();
            RightAttack();
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    private void LeftAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            time = timer;
            PlayerManager.instance.SetAttack();
            OnAttack?.Invoke(0);
        }
    }

    private void RightAttack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            time = timer;
            PlayerManager.instance.SetAttack();
            OnAttack?.Invoke(1);
        }
    }
}

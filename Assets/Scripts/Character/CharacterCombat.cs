using System;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    public static event Action<int> OnAttack;
    [SerializeField] private int damage = 25;

    private void Update()
    {
        LeftAttack();

        RightAttack();
    }

    private void LeftAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnAttack?.Invoke(0);
        }
    }

    private void RightAttack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            OnAttack?.Invoke(1);
        }
    }
}

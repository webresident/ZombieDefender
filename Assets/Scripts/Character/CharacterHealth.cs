using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public static event Action<bool> OnPlayerDeath;

    [SerializeField] private Transform respawnPosition;
    [SerializeField] private CharacterController controller;

    [SerializeField] private int health;
    [SerializeField] private float respawnTime = 2f;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        health = 100;

        Enemy.OnPlayerHit += GetDamage;
    }

    private void GetDamage(int damage)
    {
        anim.SetTrigger("isDamaged");

        health -= damage;

        print(health);

        if (health <= 0)
        {
            OnPlayerDeath.Invoke(true);
            Invoke("Respawn", respawnTime);
            anim.SetTrigger("isDeath");
        }
    }

    private void Respawn()
    {
        health = 100;
        controller.enabled = false;
        transform.position = respawnPosition.position;
        controller.enabled = true;

        anim.Rebind();
        anim.Update(0);
        OnPlayerDeath.Invoke(false);
    }

    private void OnDisable()
    {
        Enemy.OnPlayerHit -= GetDamage;
    }
}

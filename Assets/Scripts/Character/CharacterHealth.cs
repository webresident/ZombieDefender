using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public static event Action<bool> OnPlayerDeath;

    [SerializeField] private Transform respawnPosition;
    [SerializeField] private CharacterController controller;

    [SerializeField] private int health;
    [SerializeField] private float respawnTime = 2f;

    private bool isBlock = false;
    private bool isDead = false;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        health = 100;

        Enemy.OnPlayerHit += GetDamage;

        CharacterAnimation.OnBlock += SetBlock;
    }

    private void GetDamage(int damage)
    {
        if (isBlock == true)
        {
            return;
        }

        if (health > 0)
        {
            health -= damage;
            anim.SetTrigger("isDamaged");
            //print(health);
        }

        if(health <= 0 && !isDead)
        {
            isDead = true;
            controller.enabled = false;
            OnPlayerDeath.Invoke(true);
            Invoke("Respawn", respawnTime);
            anim.SetTrigger("isDeath");
        }
    }

    private void Respawn()
    {
        health = 100;
        transform.position = respawnPosition.position;
        controller.enabled = true;

        anim.Rebind();
        anim.Update(0);
        OnPlayerDeath.Invoke(false);
        isDead = false;
    }

    private void SetBlock(bool state)
    {
        isBlock = state;
    }

    private void OnDisable()
    {
        Enemy.OnPlayerHit -= GetDamage;

        CharacterAnimation.OnBlock -= SetBlock;
    }
}

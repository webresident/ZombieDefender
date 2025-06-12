using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public static event Action<bool> OnPlayerDeath;

    [SerializeField] private Transform respawnPosition;
    [SerializeField] private CharacterController controller;

    [SerializeField] private int health;
    [SerializeField] private float respawnTime = 2f;

    [SerializeField] private Slider playerSlider;
    [SerializeField] private TextMeshProUGUI amoutHealthPoints;

    private bool isBlock = false;
    private bool isDead = false;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        health = 100;
        playerSlider.value = health;

        Enemy.OnPlayerHit += GetDamage;

        CharacterAnimation.OnBlock += SetBlock;
    }

    private void GetDamage(int damage, bool isFrontHit)
    {
        if (isBlock == true && isFrontHit)
        {
            return;
        }

        if (health > 0)
        {
            anim.SetTrigger("isDamaged");
            health -= damage;
            
            playerSlider.value -= damage;
            amoutHealthPoints.text = health.ToString();

            PlayerManager.instance.GetHit();
        }

        if(health <= 0 && !isDead)
        {
            playerSlider.gameObject.SetActive(false);
            amoutHealthPoints.gameObject.SetActive(false);

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

        playerSlider.value = 100;
        playerSlider.gameObject.SetActive(true);

        amoutHealthPoints.text = 100.ToString();
        amoutHealthPoints.gameObject.SetActive(true);
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

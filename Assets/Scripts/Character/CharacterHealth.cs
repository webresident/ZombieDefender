using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int health;

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
        if (health <= 0)
        {
            anim.SetTrigger("isDeath");
        }
    }

    private void OnDisable()
    {
        Enemy.OnPlayerHit -= GetDamage;
    }
}

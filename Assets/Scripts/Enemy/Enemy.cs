using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<int> OnPlayerHit;

    [SerializeField] private GameObject handHitObject;
    
    private Animator anim;

    public string UniqueID { get; set; }
    public int health = 0;

    public int damage;

    private void Start()
    {
        anim = GetComponent<Animator>();
        SwitchOffAttack();

        GenerateUniqueID();
        
        health = 100;

        Sword.OnEnemyHit += HandlerGetAttack;
    }

    private void HandlerGetAttack(string id, int damage)
    {
        if (UniqueID == id)
        {
            anim.SetTrigger("isAttacked");
        }

        health -= damage;
        if (health <= 0)
        {
            anim.SetTrigger("isDead");
            Invoke("HandlerDeath", 1f);
        }
        print(health);
    }

    private void HandlerDeath()
    {
        Destroy(gameObject);
    }

    private void SwitchOnAttack()
    {
        handHitObject.SetActive(true);
    }

    private void SwitchOffAttack()
    {
        handHitObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnPlayerHit?.Invoke(damage);
        }
    }

    private void GenerateUniqueID()
    {
        UniqueID = Guid.NewGuid().ToString();
    }

    private void OnDisable()
    {
        Sword.OnEnemyHit -= HandlerGetAttack;
    }
}

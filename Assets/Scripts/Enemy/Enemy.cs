using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<int> OnPlayerHit;

    [SerializeField] private Collider collider;
    
    private Animator anim;

    public string uniqueID { get; set; }
    public int health = 0;

    public int damage;

    private void Start()
    {
        collider = GetComponent<Collider>();

        GenerateUniqueID();
        
        health = 100;

        Sword.OnEnemyHit += HandlerGetAttack;
    }

    private void HandlerGetAttack(string id, int damage)
    {
        if (uniqueID == id)
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
        collider.isTrigger = true;
    }

    private void SwitchOffAttack()
    {
        collider.isTrigger = false;
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
        uniqueID = Guid.NewGuid().ToString();
    }

    private void OnDisable()
    {
        Sword.OnEnemyHit -= HandlerGetAttack;
    }
}

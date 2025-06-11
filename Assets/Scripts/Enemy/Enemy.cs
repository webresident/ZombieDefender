using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static event Action<int> OnPlayerHit;

    [SerializeField] private GameObject handHitObject;
    [SerializeField] private NavMeshAgent agent;

    private Animator anim;

    public string UniqueID { get; set; }
    private int health = 0;

    private int damage = 25;

    private void Start()
    {
        print("Settings been setted");
        anim = GetComponent<Animator>();
        SwitchOffAttack();

        GenerateUniqueID();
        
        health = 100;

        Sword.OnEnemyHit += HandlerGetAttack;
    }

    private void HandlerGetAttack(string id, int damage)
    {
        if (UniqueID == id && health > 0)
        {
            health -= damage;
            print($"Zombie with ID:{UniqueID}_" + health);
            anim.SetTrigger("isAttacked");
        }

        if (health <= 0)
        {
            anim.SetTrigger("isDead");
            Invoke("HandlerDeath", 1f);
        }
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

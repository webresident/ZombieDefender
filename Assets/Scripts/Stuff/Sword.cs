using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public static event Action<string, int> OnHit;
    public static event Action<string, int> OnEnemyHit;

    [SerializeField] private int damage = 25;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            if (damageable is not IPlayerDamageable)
            {
                damageable.TakeDamage(damage);
            }
        }
        //TODO: to refactor this part of code
        if (other.CompareTag("Dummy"))
        {
            Dummy dummy = other.gameObject.GetComponent<Dummy>();
            if (dummy != null)
            {
                OnHit?.Invoke(dummy.UniqueID, damage);
            }
        }
        
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                print("Invoke decreasing");
                OnEnemyHit?.Invoke(enemy.UniqueID, damage);
            }
        }
    }
}

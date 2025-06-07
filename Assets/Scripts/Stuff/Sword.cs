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
                OnEnemyHit?.Invoke(enemy.UniqueID, damage);
            }
        }
    }
}

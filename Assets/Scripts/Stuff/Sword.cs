using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public static event Action<string, int> OnHit;

    [SerializeField] private int damage = 25;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dummy"))
        {
            Dummy dummy = other.gameObject.GetComponent<Dummy>();
            if (dummy != null)
            {
                OnHit?.Invoke(dummy.uniqueID, damage);
            }
            else
            {
                print("null nahoi");
            }
        }
    }
}

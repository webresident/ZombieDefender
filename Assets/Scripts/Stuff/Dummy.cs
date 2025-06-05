using System;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public string uniqueID { get; set; }

    private void Start()
    {
        GenerateUniqueID();

        Sword.OnHit += HandlerGetAttack;
    }

    private void HandlerGetAttack(string id, int damage)
    {
        if (uniqueID == id)
        {
            anim.SetTrigger("isAttacked");
        }
    }

    private void GenerateUniqueID()
    {
        uniqueID = Guid.NewGuid().ToString();
    }

    private void OnDisable()
    {
        Sword.OnHit -= HandlerGetAttack;
    }
}

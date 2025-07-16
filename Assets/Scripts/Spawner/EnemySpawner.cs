using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static event Action<int> OnRemove;

    [SerializeField] private Transform player;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemy;

    [SerializeField] private float radiusOfRound = 5f;
    [SerializeField] private float checkRadius = 10f;
    [SerializeField] private float spawnDelay = 5f;

    private Dictionary<string, GameObject> amountOfZombies;

    private float spawnTimer = 0f;
    private bool playerIsInside = false;

    private void Start()
    {
        amountOfZombies = new Dictionary<string, GameObject>();
        Enemy.OnDeath += RemoveFromDictionary;
    }

    private void Update()
    {
        //print(amountOfZombies.Count);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        playerIsInside = distanceToPlayer <= checkRadius;

        if (playerIsInside)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnDelay)
            {
                SpawnEnemiesInsideCircle();
                spawnTimer = 0f;
            }
        }
        else
        {
            spawnTimer = 0f;
        }
    }

    private void SpawnEnemiesInsideCircle()
    {
        int enemyCount = 3 - amountOfZombies.Count;

        if (enemyCount <= 0)
        {
            return;
        }

        for (int i = 0; i < enemyCount; i++)
        {
            Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0,spawnPoints.Length)];
            GameObject zombie = Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
            zombie.transform.SetParent(spawnPoint);

            if (zombie.TryGetComponent(out Enemy enemyObj))
            {
                amountOfZombies.Add(enemyObj.UniqueID, zombie);
            }
        }
    }

    private void RemoveFromDictionary(string id, int level)
    {
        if (amountOfZombies.ContainsKey(id))
        {
            amountOfZombies.Remove(id);
            OnRemove?.Invoke(level);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusOfRound);
    }

    private void OnDestroy()
    {
        Enemy.OnDeath -= RemoveFromDictionary;
    }
}

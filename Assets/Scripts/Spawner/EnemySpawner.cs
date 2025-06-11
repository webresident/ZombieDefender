using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemy;

    [SerializeField] private float radiusOfRound = 5f;
    [SerializeField] private float checkRadius = 10f;
    [SerializeField] private float spawnDelay = 5f;


    private float spawnTimer = 0f;
    private bool playerIsInside = false;

    private void Update()
    {
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
        int enemyCount = 3;
        for (int i = 0; i < enemyCount; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)];
            GameObject zombie = Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
            zombie.transform.SetParent(spawnPoint);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusOfRound);
    }
}

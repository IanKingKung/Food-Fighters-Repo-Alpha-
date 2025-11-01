using UnityEngine;
using System.Collections;

//purpose to spawn enemies in the game
public class EnemySpawnerBehavior : MonoBehaviour
{
    //create references to enemy prefabs
    public GameObject appleEnemyPrefab;
    public GameObject bananaEnemyPrefab;
    public GameObject donutEnemyPrefab;

    //enemy spawn rate/number variables
    private float appleSpawnInterval = 2f;
    private float bananaSpawnInterval = 8f;
    private float donutManSpawnInterval = 15f;

    //keep track of enemies remaining
    public int enemiesRemaining;
    public bool roundOver;

    //keep reference to game manager
    public GameManager gameManager;


    //should destroy all enemies if player dies (get called by gameManager?)
    /*
        - GameManager keep track of coins and state of game (if game over or not)
        - GameManager should call functions of this class to spawn in enemies
    */

    void Start()
    {
        SpawnEnemies();
    }

    void Update()
    {

    }

    void UpdateSpawnRate()
    {
        // Cancel old invokes
        CancelInvoke(nameof(SpawnApple));
        CancelInvoke(nameof(SpawnBanana));
        CancelInvoke(nameof(SpawnDonutEnemy));

        //TODO
    }

    void SpawnEnemies()
    {
        StartCoroutine(SpawnApple(2, appleSpawnInterval));
        StartCoroutine(SpawnBanana(2, bananaSpawnInterval));
        StartCoroutine(SpawnDonutEnemy(2, donutManSpawnInterval));
    }

    //Spawn enemies
    void InstantiateAtRandom(GameObject prefab, float y)
    {
        float xSpawnPos = Random.Range(-15f, 3f);
        float zSpawnPos = Random.Range(-5f, 13f);
        Vector3 spawnPos = new Vector3(xSpawnPos, y, zSpawnPos);
        Instantiate(prefab, spawnPos, transform.rotation);
    }
    IEnumerator SpawnApple(int numEnemy, float delay)
    {
        if (!gameManager.isGameOver)
        {
            for(int i = 0; i < numEnemy; i++)
            {
                InstantiateAtRandom(appleEnemyPrefab, 0.5f);
                yield return new WaitForSeconds(delay);
            }
        }
    }
    IEnumerator SpawnBanana(int numEnemy, float delay)
    {
        if (!gameManager.isGameOver)
        {
            for(int i = 0; i < numEnemy; i++)
            {
                InstantiateAtRandom(bananaEnemyPrefab, 0.5f);
                yield return new WaitForSeconds(delay);
            }
        }
    }
    IEnumerator SpawnDonutEnemy(int numEnemy, float delay)
    {
        if (!gameManager.isGameOver)
        {
            for(int i = 0; i < numEnemy; i++)
            {
                InstantiateAtRandom(donutEnemyPrefab, 1f);
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
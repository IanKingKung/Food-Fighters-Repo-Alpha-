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
    private float bananaSpawnInterval = 5f;
    private float donutManSpawnInterval = 8f;

    public int numApples;
    public int numBananas;
    public int numDonuts;

    //keep track of enemies remaining and rounds
    public int enemiesRemaining;
    public bool roundOver;

    //keep reference to game manager
    public GameManager gameManager;


    //should destroy all enemies if player dies (get called by gameManager?) YES!
    /*
        - GameManager keep track of coins and state of game (if game over or not)
        - GameManager should call functions of this class to spawn in enemies
    */

    // void Start()
    // {
    //     BeginRound();
    // }

    //set up all of our new spawn rates and numbers of enemies to spawn
    public void BeginRound(int round)
    {
        
        roundOver = false; 

        numApples = 2 + round * 3;
        numBananas = 1 + (round-3) * 2;     //spawn bananas after round 3
        numDonuts = 1 + (round-7) * 2;      //spawn donuts after round 7

        appleSpawnInterval = Mathf.Max(0.5f, 2f - 0.2f * round);
        bananaSpawnInterval = Mathf.Max(2f, 8f - 0.5f * round);
        donutManSpawnInterval = Mathf.Max(3f, 15f - 1f * round);

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        StartCoroutine(SpawnApple(numApples, appleSpawnInterval));
        StartCoroutine(SpawnBanana(numBananas, bananaSpawnInterval));
        StartCoroutine(SpawnDonutEnemy(numDonuts, donutManSpawnInterval));

        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("DonutEnemy").Length == 0);

        roundOver = true;

        //now enter buy phase
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
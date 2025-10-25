using UnityEngine;
using TMPro;
using System.Collections.Generic;


public enum GameState
{
    Playing,        //enemy's active, player active
    BuyPhase,       //enemy's inactive, player can buy items
    GameOver        //player is dead, game over
}   


public class GameManager : MonoBehaviour
{
    /*

        Game Manager doesn't need to spawn enemies or keep istances of prefabs
        Remove: enemy prefabs (not player), delete spawn intervals 



    */
    [Header("References")]
    public GameObject appleEnemyPrefab;
    public GameObject bananaEnemyPrefab;
    public GameObject donutEnemyPrefab;
    public GameObject player;
    private PlayerController playerControllerScript;
    private float startDelay = 2f;
    private float spawnInterval = 2f;
    private float bananaSpawnInterval = 8f;
    private float donutManSpawnInterval = 15f;
    public bool isGameOver; //bool to determine if the game is over


    //coin information
    public static int numCoins;
    int maxCoins = 10000;

    //play death screen if user dies
    public TextMeshProUGUI deathScreen;
    //make this change
    void Start()
    {
        isGameOver = false;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnApple", startDelay, spawnInterval);
        InvokeRepeating("SpawnBanana", startDelay, bananaSpawnInterval);
        InvokeRepeating("SpawnDonutEnemy", startDelay, donutManSpawnInterval);
    }

    void Update()
    {
        if (!playerControllerScript.isAlive)
        {
            isGameOver = true;

            //set death screen active if player dies
            deathScreen.gameObject.SetActive(true);
        }

        if (isGameOver)
        {
            DestroyAllEnemies();    //destroy all enemies when game is over
        }
    }

    void SpawnApple()
    {
        if (playerControllerScript.isAlive && !isGameOver)
        {
            float xSpawnPos = Random.Range(-15f, 3f);
            float zSpawnPos = Random.Range(-5f, 13f);
            Vector3 spawnPos = new Vector3(xSpawnPos, 0.5f, zSpawnPos);
            Instantiate(appleEnemyPrefab, spawnPos, transform.rotation);
        }
    }

    void SpawnBanana()
    {
        if (playerControllerScript.isAlive && !isGameOver)
        {
            float xSpawnPos = Random.Range(-15f, 3f);
            float zSpawnPos = Random.Range(-5f, 13f);
            Vector3 spawnPos = new Vector3(xSpawnPos, 0.5f, zSpawnPos);
            Instantiate(bananaEnemyPrefab, spawnPos, transform.rotation);
        }
    }

    void SpawnDonutEnemy()
    {
        if (playerControllerScript.isAlive && !isGameOver)
        {
            float xSpawnPos = Random.Range(-15f, 3f);
            float zSpawnPos = Random.Range(-5f, 13f);
            Vector3 spawnPos = new Vector3(xSpawnPos, 1f, zSpawnPos);
            Instantiate(donutEnemyPrefab, spawnPos, transform.rotation);
        }
    }

    void DestroyAllEnemies()
    {
        List<GameObject> enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));  //destroy all enemies 
        enemies.AddRange(GameObject.FindGameObjectsWithTag("DonutEnemy"));

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    //Coin behavior
    public void incrementCoins()
    {
        if (numCoins + 1 <= maxCoins) numCoins++;
    }
    public void increaseCoins(int num)
    {
        if (numCoins + num <= maxCoins) numCoins = maxCoins;
        else numCoins += num;
    }
    public int GetNumCoins()
    {
        return numCoins;
    }
}

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
    public GameObject player;
    private PlayerController playerControllerScript;
    private float startDelay = 2f;

    public bool isGameOver; //bool to determine if the game is over


    //coin information
    public static int numCoins;
    int maxCoins = 10000;

    //play death screen if user dies
    public TextMeshProUGUI deathScreen;

    //gain access to enemySpawner
    public EnemySpawnerBehavior enemySpawner;


    void Start()
    {
        isGameOver = false;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        //set death screen active if player dies
        if (!playerControllerScript.isAlive)
        {
            isGameOver = true;
            deathScreen.gameObject.SetActive(true);
        }

        //destroy all enemies when game is over
        if (isGameOver)
        {
            DestroyAllEnemies();
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

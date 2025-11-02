using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;


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

    //keep track of game
    public bool isGameOver; //bool to determine if the game is over
    public int gameRound;
    private int maxRound = 15;
    public GameState currentState = GameState.Playing;

    //coin information
    public static int numCoins;
    int maxCoins = 10000;

    //play death screen if user dies
    public TextMeshProUGUI deathScreen;

    //gain access to enemySpawner
    public EnemySpawnerBehavior enemySpawner;

    //access to buyphase UI
    public BuyPhaseBehavior buyPhaseUI;

    //access to its sound
    public AudioSource audioSource;
    public AudioClip bellSound;

    //access to next scene
    private string nextSceneName = "GameOverScene";


    void Start()
    {
        gameRound = 1; 
        isGameOver = false;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(PlayRound());
    }

    void Update()
    {
        //set death screen active if player dies
        if (!playerControllerScript.isAlive)
        {
            isGameOver = true;
            currentState = GameState.GameOver;
            DestroyAllEnemies();
            deathScreen.gameObject.SetActive(true);
        }

        if (currentState == GameState.Playing && enemySpawner.roundOver)
        {
            enemySpawner.roundOver = false; 
            StartCoroutine(BuyPhase());
        }
    }

    //play the game round
    public IEnumerator PlayRound()
    {
        if(gameRound > maxRound) SceneManager.LoadScene(nextSceneName);
        yield return StartCoroutine(BellsRinging());
        Debug.Log($"StartRound {gameRound}");
        if (isGameOver) yield break;
        currentState = GameState.Playing;
        enemySpawner.BeginRound(gameRound);
    }

    //buy phase where user gets 25 seconds to buy their upgrades
    IEnumerator BuyPhase()
    {
        enemySpawner.roundOver = false; 
        yield return StartCoroutine(BellsRinging());
        DestroyAllEnemies();
        // Debug.Log($"Buy phase started for {gameRound}");
        currentState = GameState.BuyPhase;

        playerControllerScript.playerInBuyPhase = true;

        float buyDuration = 20f;
        buyPhaseUI.Initialize(this, buyDuration);

        yield return new WaitForSeconds(buyDuration);
        buyPhaseUI.gameObject.SetActive(false);

        // Debug.Log("Buy phase ended");
        playerControllerScript.playerInBuyPhase = false;

        gameRound++;
        if (gameRound >= maxRound)
        {
            //CONGRATS YOU WON THE GAME
            yield return StartCoroutine(SevenBellsRinging());
            SceneManager.LoadScene(nextSceneName);
            yield break;
        }
        else
        {
            yield return StartCoroutine(PlayRound());
        }
    }

    IEnumerator BellsRinging()
    {
        for (int i = 0; i < 3; i++)
        {
            audioSource.PlayOneShot(bellSound);
            yield return new WaitForSeconds(2.3f);
        }

    }
    
    IEnumerator SevenBellsRinging()
    {
        for(int i = 0; i < 7; i++)
        {
            audioSource.PlayOneShot(bellSound);
            yield return new WaitForSeconds(2.3f);
        }
        
    }


    //destroy all enemy game objects when player dies
    void DestroyAllEnemies()
    {
        List<GameObject> enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));  //destroy all enemies 
        enemies.AddRange(GameObject.FindGameObjectsWithTag("DonutEnemy"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("FullBody"));

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

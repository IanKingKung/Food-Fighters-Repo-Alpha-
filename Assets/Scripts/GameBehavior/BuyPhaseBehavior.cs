using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuyPhaseBehavior : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    // public TextMeshProUGUI coinText;

    public Button healthUpgrade;
    public Button buyPlates;
    public Button plateSpeedUpgrade;
    public Button plateCapacityUpgrade;
    public Button manaCapacityUpgrade;
    public Button buyBigPotSpell;
    public Button buyDash;
    public Button buyKnifeShieldSpell;

    private float buyPhaseDuration;
    private float timeLeft;
    public Button endBuyPhaseEarly;

    //access to gameManager and playerController
    private GameManager gameManager;
    public PlayerController playerControllerScript;

    //access to sound
    public AudioSource audioSource;
    public AudioClip buySound;

    public void Initialize(GameManager manager, float duration)
    {
        gameManager = manager;
        buyPhaseDuration = duration;
        timeLeft = duration;
        gameObject.SetActive(true);

        // Update coin display
        // coinText.text = $"Coins: {GameManager.numCoins}";
        // Hook up button events
        healthUpgrade.onClick.AddListener(() => BuyHealth());
        buyPlates.onClick.AddListener(() => BuyPlates());
        plateSpeedUpgrade.onClick.AddListener(() => BuyPlateSpeed());
        plateCapacityUpgrade.onClick.AddListener(() => UpgradePlateCapacity());
        manaCapacityUpgrade.onClick.AddListener(() => BuyManaCapacity());
        buyBigPotSpell.onClick.AddListener(() => BuyBigPotSpell());
        buyDash.onClick.AddListener(() => BuyDash());
        buyKnifeShieldSpell.onClick.AddListener(() => BuyKnifeShieldSpell());

        endBuyPhaseEarly.onClick.AddListener(() => EndBuyPhase());


    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = $"Time Left: {timeLeft:F1}s";
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void EndBuyPhase()
    {
        timeLeft = 0;

        gameObject.SetActive(false);

        // Tell GameManager to continue
        playerControllerScript.playerInBuyPhase = false;
        gameManager.StopAllCoroutines();
        gameManager.gameRound++;
        gameManager.PlayRound();
    }

    void BuyHealth()
    {
        int cost = 5;
        if (GameManager.numCoins >= cost && playerControllerScript.currentHealth < playerControllerScript.maxHealth)
        {
            audioSource.PlayOneShot(buySound);
            GameManager.numCoins -= cost;
            playerControllerScript.AddHealth(10);
            // coinText.text = $"Coins: {GameManager.numCoins}";
        }
    }

    void BuyPlates()
    {
        int cost = 2;
        if (GameManager.numCoins >= cost && playerControllerScript.numPlates < playerControllerScript.maxPlates)
        {
            audioSource.PlayOneShot(buySound);
            GameManager.numCoins -= cost;
            playerControllerScript.AddPlates(5);
            // coinText.text = $"Coins: {GameManager.numCoins}";
        }
    }

    void BuyPlateSpeed()
    {
        int cost = 15;
        if (GameManager.numCoins >= cost)
        {
            if (playerControllerScript.spawnPlateCooldown - 0.05 >= 0.1)
            {
                GameManager.numCoins -= cost;
                audioSource.PlayOneShot(buySound);
                playerControllerScript.spawnPlateCooldown -= 0.05f;
            }
            // coinText.text = $"Coins: {GameManager.numCoins}";
        }
    }

    void UpgradePlateCapacity()
    {
        int cost = 5;
        if (GameManager.numCoins >= cost)
        {
            
            if (playerControllerScript.maxPlates + 5 <= 100)
            {
                GameManager.numCoins -= cost;
                audioSource.PlayOneShot(buySound);
                playerControllerScript.maxPlates += 5;
            }
            // coinText.text = $"Coins: {GameManager.numCoins}";
        }
    }

    void BuyManaCapacity()
    {
        int cost = 10;
        if (GameManager.numCoins >= cost)
        {
            
            if (playerControllerScript.maxMagic + 10 <= 200)
            {
                GameManager.numCoins -= cost;
                audioSource.PlayOneShot(buySound);
                playerControllerScript.maxMagic += 5;
            }
            // coinText.text = $"Coins: {GameManager.numCoins}";
        }
    }

    void BuyBigPotSpell()
    {
        int cost = 30;
        if (GameManager.numCoins >= cost && !playerControllerScript.PlayerOwnsBigPot)
        {
            audioSource.PlayOneShot(buySound);
            GameManager.numCoins -= cost;
            playerControllerScript.PlayerOwnsBigPot = true;
            // coinText.text = $"Coins: {GameManager.numCoins}";
        }
    }

    void BuyKnifeShieldSpell()
    {
        int cost = 50;
        if (GameManager.numCoins >= cost && !playerControllerScript.PlayerOwnsKnifeShield)
        {
            audioSource.PlayOneShot(buySound);
            GameManager.numCoins -= cost;
            playerControllerScript.PlayerOwnsKnifeShield = true;
            // coinText.text = $"Coins: {GameManager.numCoins}";
        }
    }
    
    void BuyDash()
    {
        int cost = 15;
        if (GameManager.numCoins >= cost && !playerControllerScript.PlayerOwnsDash)
        {
            audioSource.PlayOneShot(buySound);
            GameManager.numCoins -= cost;
            playerControllerScript.PlayerOwnsDash = true;
            // coinText.text = $"Coins: {GameManager.numCoins}";
        }
    }

}

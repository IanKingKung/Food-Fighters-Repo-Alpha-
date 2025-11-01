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

    //access to gameManager and playerController
    private GameManager gameManager;
    public PlayerController playerControllerScript;

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

    void BuyHealth()
    {
        int cost = 5;
        if (GameManager.numCoins >= cost && playerControllerScript.currentHealth < playerControllerScript.maxHealth)
        {
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
            GameManager.numCoins -= cost;
            if (playerControllerScript.spawnPlateCooldown - 0.1 >= 0.1)
            {
                playerControllerScript.spawnPlateCooldown -= 0.1f;
            }
            // coinText.text = $"Coins: {GameManager.numCoins}";
        }
    }

    void UpgradePlateCapacity()
    {
        int cost = 5;
        if (GameManager.numCoins >= cost)
        {
            GameManager.numCoins -= cost;
            if (playerControllerScript.maxPlates + 5 <= 100)
            {
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
            GameManager.numCoins -= cost;
            if (playerControllerScript.maxMagic + 10 <= 200)
            {
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
            GameManager.numCoins -= cost;
            playerControllerScript.PlayerOwnsDash = true;
            // coinText.text = $"Coins: {GameManager.numCoins}";
        }
    }

}

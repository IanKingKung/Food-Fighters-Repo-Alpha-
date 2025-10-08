using UnityEngine;
using TMPro;

public class AmmoCounter : MonoBehaviour
{
    public PlayerController PlayerControllerScript;
    //plates
        private int maxPlates = 30;
        private int currentPlates;
    //magic
        private int currentMagic;
        private int maxMagic = 50;
        public MagicController magicControllerScript;
    //coins
        public GameManager gameManagerScript;
        private int currentCoins;


    //our literal texts
    public TextMeshProUGUI plateAmmoText;
    public TextMeshProUGUI magicIndicator;
    public TextMeshProUGUI coinAmountText;

    void Start()
    {
        currentPlates = maxPlates;
        currentMagic = maxMagic;
    }

    void Update()
    {
        //update currentPlates every frame
            currentPlates = PlayerControllerScript.GetNumPlates();
            UpdatePlateCount();
        
        //update magic count continuously
            currentMagic = PlayerControllerScript.GetMagic();
            UpdateMagic();

        //update coin count continuously
        UpdateCoinCount();
    }

    public void UpdatePlateCount()
    {
        plateAmmoText.text = "Plates: " + PlayerControllerScript.numPlates + "/" + PlayerControllerScript.maxPlates;
    }
    public void UpdateMagic()
    {
        magicIndicator.text = "Magic: " + currentMagic + "/" + maxMagic;
        magicControllerScript.UpdateMagicBar(currentMagic, maxMagic);
    }
    public void UpdateCoinCount()
    {
        currentCoins = gameManagerScript.GetNumCoins();
        coinAmountText.text = "Coins: " + currentCoins;
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class MagicController : MonoBehaviour
{

    [SerializeField] private Image healthBarSprite;
    [SerializeField] private float reduceSpeed = 2f;
    private float target = 1f;

    public TextMeshProUGUI magicAmountText;

    //access to player's stats
        private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }

    //update the health bar
    public void UpdateMagicBar(int maxMagic, int currentMagic)
    {
        magicAmountText.text = "Magic: " + playerControllerScript.currentMagic + "/" + playerControllerScript.maxMagic;
        target = (float)playerControllerScript.currentMagic / (float)playerControllerScript.maxMagic;   //float for precision
    }
}

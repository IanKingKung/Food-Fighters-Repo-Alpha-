using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    [SerializeField] private Image healthBarSprite;
    [SerializeField] private float reduceSpeed = 2f;
    private float target = 1f;

    //update the health bar
    public void UpdatePlayerHealthBar(int maxHealth, int currentHealth)
    {
        target = (float)currentHealth / (float)maxHealth;   //float for precision
    }

    void Start()
    {

    }

    void Update()
    {
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
}

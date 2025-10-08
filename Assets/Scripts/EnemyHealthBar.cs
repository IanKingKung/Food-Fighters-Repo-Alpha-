using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    [SerializeField] private Image healthBarSprite;
    [SerializeField] private float reduceSpeed = 2f;
    private float target = 1f;
    private Camera cam;

    //update the health bar
    public void UpdateHealthBar(int maxHealth, int currentHealth)
    {
        target = (float)currentHealth / (float)maxHealth;   //float for precision
    }

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        Vector3 direction = cam.transform.position - transform.position;
        direction.y = 0; // Lock the vertical rotation
        transform.rotation = Quaternion.LookRotation(-direction);
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }
}

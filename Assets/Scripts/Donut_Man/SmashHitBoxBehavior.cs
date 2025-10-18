using UnityEngine;

public class SmashHitBoxBehavior : MonoBehaviour
{
    public PlayerController playerControllerScript;
    public GameObject smashHitEffect;
    public int smashDamage = 15;

    void Start()
    {
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Instantiate(smashHitEffect, transform.position, transform.rotation * Quaternion.Euler(90f, 0f, 0f));
        Destroy(gameObject, 0.2f);
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Player in Smash");
            playerControllerScript.PlayerTakeDamage(smashDamage);
            playerControllerScript.StartCoroutine(playerControllerScript.SlowEffect(2)); //apply slow down effect to player
        }
    }
}

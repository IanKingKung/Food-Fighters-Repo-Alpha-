using UnityEngine;
using System.Collections;

public class DonutManProj : MonoBehaviour
{
    //effects 
    public GameObject explosionEffect;

    //access to hitbox script
    public DonutManProjHitbox hitbox;

    //access to player controller
    private PlayerController playerControllerScript;

    //constants
    private int projDamage = 15;

    //our sound effects
    public AudioSource audioSource;
    public AudioClip explosionSound;

    void Start()
    {
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        hitbox = GetComponentInChildren<DonutManProjHitbox>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        {
            if(other.gameObject.CompareTag("Floor"))
            {
                StartCoroutine(BlowUpAfterTime(2f));
            }
        }
    }

    private IEnumerator BlowUpAfterTime(float num)
    {
        yield return new WaitForSeconds(num);

        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        if (hitbox.isPlayerInRange)
        {
            //deal damage to player here
            playerControllerScript.PlayerTakeDamage(projDamage);
        }
        audioSource.PlayOneShot(explosionSound);
        Destroy(gameObject, 0.1f);  //destroy projectile after a short delay for sound
        
    }
}

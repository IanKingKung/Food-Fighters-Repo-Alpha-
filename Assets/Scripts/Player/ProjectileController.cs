//control the behavior of the player's projectiles
using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{

    //animations
        public GameObject bigPotExplosion;
        private bool hasExploded = false;

    //sound clips
        public AudioSource audioSource;
        public AudioClip bigPotHitSound;

    void Start()
    {

    }

    void Update()
    {
        //destroy gameObjects based on behavior 
        if (gameObject.CompareTag("bigpot"))
        {
            Destroy(gameObject, 1.8f);
        }
        else if (gameObject.CompareTag("bigPotIndicator"))
        {
            Destroy(gameObject, 1.8f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!hasExploded && gameObject.CompareTag("bigpot"))
        {
            audioSource.PlayOneShot(bigPotHitSound);
            SpawnExplosion();
            hasExploded = true;
        }
        
        if (gameObject.CompareTag("plate"))
        {
            
            if (other.gameObject.CompareTag("Floor"))
            {
                Destroy(gameObject);
            }
        }
    }

    //spawn explosion
    private void SpawnExplosion()
    {
        Instantiate(bigPotExplosion, transform.position, Quaternion.identity);
    }
}

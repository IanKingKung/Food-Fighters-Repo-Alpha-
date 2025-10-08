using UnityEngine;
using System.Collections;

public class CoinBehavior : MonoBehaviour
{
    //rotate speed for coin animation
        public float rotationSpeed = 10f;

    public PlayerController PlayerControllerScript;
    public GameManager gameManagerScript;


    //play sound when picked up
    public AudioSource audioSource;
    public AudioClip coinPickUpSound;

    void Start()
    {
        //assign PlayerControllerScript
        PlayerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();


        StartCoroutine(Rotate());
    }

    void Update()
    {
        
    }

    public IEnumerator Rotate()
    {
        while(true)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(coinPickUpSound);
            Destroy(gameObject, coinPickUpSound.length);
            gameManagerScript.incrementCoins();
            
        }
    }
}

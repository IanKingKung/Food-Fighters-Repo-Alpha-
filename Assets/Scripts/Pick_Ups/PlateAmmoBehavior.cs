using UnityEngine;
using System.Collections;

public class PlateAmmoBehavior : MonoBehaviour
{
    public float rotationSpeed = 20f;

    //oscillation
        public float floatAmplitude = 0.22f;  
        public float floatFrequency = 1f;     
        private Vector3 startPos;

    private PlayerController playerControllerScript;

    //play sound when picked up
    public AudioSource audioSource;
    public AudioClip plateAmmoSound;

    void Start()
    {
        startPos = transform.position; 
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        StartCoroutine(Rotate());
    }

    void Update()
    {
        // Floating up and down using sine wave
            float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = startPos + new Vector3(0f, yOffset, 0f);
    }

    public IEnumerator Rotate()
    {
        while (true)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && (playerControllerScript.numPlates < playerControllerScript.maxPlates))
        {
            audioSource.PlayOneShot(plateAmmoSound);
            playerControllerScript.AddPlates(7);
            Destroy(gameObject, plateAmmoSound.length);
            
        }
    }
}

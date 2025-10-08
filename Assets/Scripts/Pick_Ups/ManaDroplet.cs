using UnityEngine;
using System.Collections;

public class ManaDroplet : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private int magicDropletValue = 3;

    //floating variables
    public float floatAmplitude = 0.22f;
    public float floatFrequency = 1f;
    Vector3 startPos;

    //play sound when picked up
    public AudioSource audioSource;
    public AudioClip manaPickUpSound;

    void Start()
    {
        startPos = transform.position;
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = startPos + new Vector3(0f, yOffset, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && (playerControllerScript.currentMagic < playerControllerScript.maxMagic))
        {
            // Debug.Log(playerControllerScript.GetMagic());
            audioSource.PlayOneShot(manaPickUpSound);
            Destroy(gameObject, manaPickUpSound.length);
            playerControllerScript.IncreaseMagic(magicDropletValue);
            
            // Debug.Log("after: " + playerControllerScript.GetMagic());
        }
    }
}

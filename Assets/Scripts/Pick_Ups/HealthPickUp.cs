using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    private PlayerController playerControllerScript;

    //floating variables
    public float floatAmplitude = 0.22f;
    public float floatFrequency = 1f;
    Vector3 startPos;

    void Start()
    {
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        startPos = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = startPos + new Vector3(0f, yOffset + 0.1f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            playerControllerScript.AddHealth(15);
        }
    }
}

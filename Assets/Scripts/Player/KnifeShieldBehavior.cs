using UnityEngine;

public class KnifeShieldBehavior : MonoBehaviour
{
    private PlayerController playerControllerScript;
    Vector3 offset = new Vector3(0.23f, 1.5f, 1.39f);


    void Start()
    {
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        transform.position = playerControllerScript.transform.position + offset;
    }
}

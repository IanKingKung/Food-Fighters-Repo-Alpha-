using UnityEngine;

public class DonutManBodyBehavior : MonoBehaviour
{
    Vector3 direction;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        direction = (player.transform.position - gameObject.transform.position).normalized; //direction points at player's location
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
    }
}

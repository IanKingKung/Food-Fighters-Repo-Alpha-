//controls camera that follows main player
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 shakeOffset;   //vector that captures the shake option
    private Vector3 offset = new Vector3(0, 6, -3); //offset to follow the player

    void Start()
    {

        
        
        
    }

    void Update()
    {
        //position equals player's position plus offset
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform);


    }

    // public IEnumerator ShakeCamera(float x_magnitude, float y_magnitude, float duration)
    // {
    //     float elapsed = 0f;
       
    //     while (elapsed < duration)
    //     {
    //         float x = Random.Range(-x_magnitude, x_magnitude);
    //         float y = Random.Range(-y_magnitude, y_magnitude);

    //         shakeOffset = new Vector3(transform.position.x + x, transform.position.y + y, -3);
    //         transform.position = shakeOffset;

    //         elapsed += Time.deltaTime;
    //         yield return null;
            
    //     }

    //     // Reset shake when done
    //     shakeOffset = Vector3.zero;
    // }
}

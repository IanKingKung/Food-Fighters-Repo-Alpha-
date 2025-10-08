using UnityEngine;

public class DonutManProj : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"));
        {
            Destroy(gameObject);
        }
    }
}

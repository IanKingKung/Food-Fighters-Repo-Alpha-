using UnityEngine;
using System.Collections;

public class BananaProjBehavior : MonoBehaviour
{
    public int speed = 3;

    //animations
    public GameObject deathEffect;

    void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);    //will deal damage to the player in the player script
            //play some animation
        }
    }

    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

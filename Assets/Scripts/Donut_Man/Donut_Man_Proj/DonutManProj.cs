using UnityEngine;
using System.Collections;

public class DonutManProj : MonoBehaviour
{
    //effects 
    public GameObject explosionEffect;

    //access to hitbox script
    public DonutManProjHitbox hitbox;

    //access to player controller
    private PlayerController playerControllerScript;

    //constants
    private int projDamage = 15;

    void Start()
    {
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        hitbox = GetComponentInChildren<DonutManProjHitbox>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        {
            if (other.gameObject.CompareTag("Floor")) ;
            {
                StartCoroutine(BlowUpAfterTime(2f));
            }
        }
    }

    private IEnumerator BlowUpAfterTime(float num)
    {
        yield return new WaitForSeconds(num);
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        if(hitbox.isPlayerInRange)
        {
            //deal damage to player here
            playerControllerScript.PlayerTakeDamage(projDamage);
        }
        Destroy(gameObject);
    }
}

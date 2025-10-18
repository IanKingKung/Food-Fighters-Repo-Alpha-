using UnityEngine;
using UnityEngine.AI;
using System.Collections;
//donut man enemy general behavior test comment

public class DonutManBehavior : MonoBehaviour
{
    public GameObject smashHitBox;
    private Vector3 smashHitBoxSpawnPosition;

    //cooldown variables
    private float smashAttackCooldown = 1f;
    private bool canAttack;

    //sound
    public AudioSource audioSource;
    public AudioClip smashSound;

    //access to donut_sensor
    public SensorBehavior SensorBehaviorScript;

    //movement variables
    public GameObject player;
    public Transform head;
    private Vector3 direction;
    public float speed;

    //health
    [SerializeField] private EnemyHealthBar healthBar;
    [SerializeField] private int maxHealth = 70;
    [SerializeField] private int currentHealth;

    //effects
    public GameObject plateHitEffect;
    public GameObject donutDeathEffect;

    //loot and drops 
    public GameObject coinPrefab;
    public GameObject ammoPlatePrefab;
    public GameObject manaDropletPrefab;
    public GameObject healthPickup;

    //donut projectiles
    public GameObject donutProjectilesPrefab1;      //pink donut
    public GameObject donutProjectilesPrefab2;    //brown donut
    private float LaunchForce = 2.0f;


    //animations
    public Animator DonutEnemyAnimator;

    void Start()
    {
        currentHealth = 50;
        player = GameObject.FindGameObjectWithTag("Player");
        speed = 0.5f;
        canAttack = true;
    }

    void Update()
    {
        //head movement
        Vector3 targetPosition = player.transform.position + new Vector3(0f, 1.02f, 0f);
        head.LookAt(targetPosition);
        // //actual movement
        direction = (player.transform.position - gameObject.transform.position).normalized; //direction points at player's location
        Vector3 bodyPosition = transform.position + (direction * Time.deltaTime * speed);   //move object towards player at a specific speed
        bodyPosition.y = 1.02f;
        transform.position = bodyPosition;
        
        smashAttackCooldown -= Time.deltaTime;
        if (smashAttackCooldown <= 0)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        if (canAttack && SensorBehaviorScript.isPlayerInRange)
        {
            StartCoroutine(SmashAttack());
            smashAttackCooldown = 3f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("plate"))
        {
            Instantiate(plateHitEffect, transform.position + new Vector3(0f, 0.8f, 0f), Quaternion.identity);
            TakeDamage(5);
        }
        if (other.gameObject.CompareTag("bigpot"))
        {
            TakeDamage(20);
        }


    }

    private IEnumerator SmashAttack()
    {
        
        DonutEnemyAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(2.08f);     //give time to play animation

        smashHitBoxSpawnPosition = transform.position + transform.forward * 0.5f; //make sure donut smashes in front of him
        smashHitBoxSpawnPosition -= new Vector3(0f, 0.8f, 0f);
        Instantiate(smashHitBox, smashHitBoxSpawnPosition, Quaternion.Euler(0f, 0f, 0f));

        audioSource.PlayOneShot(smashSound);

        // DonutEnemyAnimator.SetTrigger("Attack");
    }

    public void TakeDamage(int num)
    {
        currentHealth -= num;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        Instantiate(plateHitEffect, transform.position, transform.rotation);
        if (currentHealth <= 0) DonutDie();
    }

    public void DonutDie()
    {
        //drop the loot
        int randomInt = Random.Range(1, 3);
        Vector3 targetPosition = transform.position - new Vector3(0f, 0.5f, 0f) + new Vector3(Random.Range(0f, 0.3f), 0f, Random.Range(0f, 0.3f));
        for (int i = 0; i < randomInt; i++)
        {
            Instantiate(manaDropletPrefab, targetPosition, transform.rotation);
        }
        Instantiate(coinPrefab, targetPosition, transform.rotation);
        Instantiate(ammoPlatePrefab, targetPosition, transform.rotation);

        Instantiate(healthPickup, transform.position, transform.rotation);
        // Instantiate(healthPickup, transform.position, transform.rotation);

        Instantiate(donutDeathEffect, transform.position, transform.rotation);
        Destroy(gameObject);

        //don't forget about the donut bombs!
        LaunchProjectiles();
    }

    //Make own separate function to spawn donut projectiles
    private void LaunchProjectiles()
    {
        GameObject proj1 = Instantiate(donutProjectilesPrefab1, transform.position + new Vector3(0f, 1.5f, 0f), Quaternion.identity);
        GameObject proj2 = Instantiate(donutProjectilesPrefab1, transform.position + new Vector3(0f, 1.5f, 0f), Quaternion.identity);
        proj1.GetComponent<Rigidbody>().AddForce(Vector3.up * LaunchForce, ForceMode.Impulse);
        proj2.GetComponent<Rigidbody>().AddForce(Vector3.up * LaunchForce, ForceMode.Impulse);

    }

}

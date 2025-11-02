//dictates behavior of basic apples
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //give apples abilities to drop materials
        public GameObject coinPrefab;
        public GameObject ammoPlatePrefab;
        public GameObject manaDropletPrefab;
        private Vector3 manaSpawnPosition;


    private GameObject player;
    private Vector3 direction;

    //apple stats
    private float speed = 1.1f;

    //access to our healthbar
        [SerializeField] private EnemyHealthBar healthBar;
        [SerializeField] private int maxHealth = 10;
        [SerializeField] private int currentHealth;

    //effects
    public GameObject deathEffect;
    public GameObject plateHitEffect;
    public GameObject spawnEffect;

    //access to sounds
    public AudioSource audioSource;
    public AudioClip spawnSound;


    void Start()
    {
        Instantiate(spawnEffect, transform.position - new Vector3(0f, 0.2f, 0f), Quaternion.Euler(90f, 90f, 90f));
        audioSource.PlayOneShot(spawnSound);
        player = GameObject.FindWithTag("Player");
        currentHealth = maxHealth;
    }

    void Update()
    {
        direction = (player.transform.position - gameObject.transform.position).normalized; //direction points at player's location
        Vector3 targetPosition = transform.position + direction * Time.deltaTime * speed;   //move object towards player at a specific speed

        targetPosition.y = 0.5f;

        transform.position = targetPosition;

        if (currentHealth <= 0) Die();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("plate"))
        {
            Instantiate(plateHitEffect, transform.position, Quaternion.identity);
            currentHealth -= 5;
        }
        if(other.gameObject.CompareTag("bigpot"))
        {
            currentHealth -= 20;
        }

        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        if (currentHealth <= 0) Die();
        
    }

    public void TakeDamage(int num)
    {
        Instantiate(plateHitEffect, transform.position, Quaternion.identity);
        currentHealth -= num;
    }

    public void UpdateHealth()  //use to update health when knifed
    {
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    private void Die()
    {
        //play death animation
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        //spawn coin & ammo based on 1 in 5 chance if apple dies
        int randomInt = Random.Range(0, 4);
        if (randomInt == 0 || randomInt == 1)
        {
            Instantiate(coinPrefab, transform.position, transform.rotation);
        }

        //spawn ammo in 1 in four chance
        if (randomInt == 2) Instantiate(ammoPlatePrefab, transform.position, transform.rotation);

        //spawn mana droplet in 1 in four chance
        if (randomInt == 3)
        {
            manaSpawnPosition = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
            Instantiate(manaDropletPrefab, manaSpawnPosition, transform.rotation);
        }

        
        

        Destroy(gameObject);    //destroy enemy if comes into contact with player projectile
    }
}

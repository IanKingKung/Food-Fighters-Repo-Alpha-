using UnityEngine;
using System.Collections;

public class BananaBehavior : MonoBehaviour
{
    private GameObject player;
    public GameObject bananaProjectile;


    //our banana cooldown variables
        private float shootingCooldown = 0f;   //banana projectile cooldown;
        private bool canShoot = false;

    //banana health
    [SerializeField] private EnemyHealthBar healthBar;  //get reference to our healthbar
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private int currentHealth;

    //banana death variables
        public GameObject ammoPlatePrefab;
        public GameObject coinPrefab;
        public GameObject manaDropletPrefab;

    //animations
        public GameObject deathEffect;
        public GameObject hitEffect;
        public GameObject shootEffect;
        public Animator bananaEnemyAnimator;

    //sound effects
    public AudioSource audioSource;
    public AudioClip shootSound;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canShoot = true;
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    void Update()
    {

        shootingCooldown -= Time.deltaTime;
        if (shootingCooldown <= 0f)
        {
            canShoot = true;
            shootingCooldown = 5f;
        }
        Vector3 direction = (player.transform.position - transform.position).normalized;    //for the projectiles
        transform.rotation = Quaternion.LookRotation(direction);

        if (canShoot)
        {
            Attack();
            canShoot = false;
        }
        

        if (currentHealth <= 0) BananaDie();
    }

    public void Attack()
    {
        audioSource.PlayOneShot(shootSound);
        bananaEnemyAnimator.SetTrigger("Fire");
        Instantiate(shootEffect, transform.position + Vector3.forward, transform.rotation);
        Instantiate(bananaProjectile, transform.position, transform.rotation);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("plate"))
        {
            currentHealth -= 5;
        }
        else if (other.gameObject.CompareTag("bigpot"))
        {
            currentHealth -= 20;
        }

        if (currentHealth == 0)
        {
            BananaDie();
        }
        healthBar.UpdateHealthBar(maxHealth, currentHealth);

    }
    
    public void TakeDamage(int num)
    {
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        currentHealth -= num;
    }

    public void UpdateHealth()  //use to update health when knifed
    {
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }
    
    private void BananaDie()
    {
        //spawn coin & ammo based on 1 in 5 chance if apple dies
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        int randomInt = Random.Range(0, 3);
        if (randomInt == 1) Instantiate(ammoPlatePrefab, transform.position, transform.rotation);
        if (randomInt == 2)
        {
            Instantiate(manaDropletPrefab, transform.position + new Vector3(0.1f, 0f, 0f), transform.rotation);
            Instantiate(manaDropletPrefab, transform.position + new Vector3(0.2f, 0f, 0.2f), transform.rotation);
        }    

        Instantiate(coinPrefab, transform.position + new Vector3(0f, 0f, 0.1f), transform.rotation);

        Destroy(gameObject);    //destroy enemy if comes into contact with player projectile
    }
}

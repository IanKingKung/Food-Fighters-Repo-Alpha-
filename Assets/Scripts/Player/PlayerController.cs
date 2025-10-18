using UnityEngine;
using System.Collections;   //for the IEnumerator and CoRoutine

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float speed = 4.7f;
    private float launchForce = 16.0f;
    private Vector3 lastMoveDirection;
    private Vector3 moveDirection;
    public GameObject plateProj;
    public GameObject bigPotPrefab;
    public GameObject bigPotIndicatorPrefab;
    private TrailRenderer trail;
    Rigidbody rb;
    private bool canDash;
    private float dashCooldown = 0.4f;
    private float dashSpeed = 25f;


    //player's health
    [SerializeField] private PlayerHealthBar healthBar;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    public bool isAlive = true;
    public bool isFullHealth = false;

    //handle the aiming mechanic
    public Vector3 aimDirection { get; private set; }

    //spawn plate cooldown & conditions
    private bool canSpawnPlate = false;     //spawn plate cooldown
    private float spawnPlateCooldown = 0.5f;
    public int maxPlates = 30;
    public int numPlates;
    private bool hasPlates = true;


    //magic statistics
    public int currentMagic;
    public int maxMagic = 50;
    public bool hasMagic = true;
    //spawn bigPot cooldown & conditions
    private Vector3 clampedTargetPosition;
    private bool canSpawnBigPot = false;
    private float spawnBigPotCooldown = 3f;
    private int magicCostBigPot = 5;

    //knife shield activation
    [SerializeField] private GameObject knifeShield;    //get access to our knife shield
    [SerializeField] private bool isShieldActive;
    private Vector3 offset = new Vector3(0.23f, 1.5f, 1.39f);
    private int magicCostKnifeShield = 15;
    public GameObject KnifeShieldSummoningEffect;


    //player audio clips when clicking different buttons
    public AudioSource audioSource;
    public AudioClip plateSound;
    public AudioClip bigPotSound;
    public AudioClip knifeUnsheath;
    public AudioClip playerDash;

    //player effects
    public GameObject playerhitEffect;

    void Start()
    {
        isAlive = true;
        currentHealth = maxHealth;

        //Give player their basic weapons & stats
        canSpawnPlate = true;
        canSpawnBigPot = true;
        currentMagic = maxMagic;
        numPlates = maxPlates;
        isShieldActive = false;

        rb = GetComponent<Rigidbody>();     //the player's rigidbody
        trail = GetComponent<TrailRenderer>();  //access player's trail

        trail.emitting = false;
        rb.freezeRotation = true;  //prevents player from rotating due to outside forces
        canDash = true;

    }

    void Update()
    {
        AimAtMouse();


        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);        //move player in direction of user input

        if (moveDirection != Vector3.zero)
        {
            lastMoveDirection = moveDirection;
            Quaternion targetRotation = Quaternion.LookRotation(lastMoveDirection);     //rotate based on direction
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 6.5f);
        }


        if (Input.GetMouseButton(0) && isAlive && canSpawnPlate && hasPlates)
        {
            SpawnPlate();
            canSpawnPlate = false;
            numPlates--;
        }
        if (Input.GetKeyDown(KeyCode.E) && isAlive && hasMagic && canSpawnBigPot)
        {
            SpawnBigPot();
            canSpawnBigPot = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
            audioSource.PlayOneShot(playerDash);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && hasMagic && isAlive && !isShieldActive)
        {
            Instantiate(KnifeShieldSummoningEffect, transform.position, Quaternion.identity);
            StartCoroutine(SpawnKnifeShield());
        }

        //check if player is Dead
        if (currentHealth <= 0)
        {
            isAlive = false;
        }
        if (currentHealth == maxHealth)
        {
            isFullHealth = true;
        }

        //reduce our cooldowns
        if (!canSpawnPlate)
        {
            spawnPlateCooldown -= Time.deltaTime;
            //reset spawnPlateCooldown
            if (spawnPlateCooldown <= 0f)
            {
                canSpawnPlate = true;
                spawnPlateCooldown = 0.2f;
            }

        }
        if (!canSpawnBigPot)
        {
            spawnBigPotCooldown -= Time.deltaTime;
            //reset bigPot cooldown
            if (spawnBigPotCooldown <= 0f)
            {
                canSpawnBigPot = true;
                spawnBigPotCooldown = 3f;
            }
        }

        if (numPlates <= 0)
        {
            hasPlates = false;
            // Debug.Log("out of plates");
        }
        if (currentMagic <= 0)
        {
            hasMagic = false;
            // Debug.Log("out of big pots");
        }
    }

    //function for player to look at where the mouse is
    void AimAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //create laser pointer
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);        //plane with normal vectors at y = 0

        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))      //tells us if our ray is hitting the plane
        {
            Vector3 targetPoint = ray.GetPoint(rayDistance);        //gets 3d point along rat at distance x
            Vector3 direction = targetPoint - transform.position;
            direction.y = 0; // Keep rotation only on horizontal plane

            float distance = direction.magnitude;
            float maxDropDistance = 15f;
            float minDropDistance = 1f;

            Vector3 fromPlayerToTarget = direction;

            if (distance > maxDropDistance)
            {
                fromPlayerToTarget = fromPlayerToTarget.normalized * maxDropDistance;
            }
            else if (distance < minDropDistance)
            {
                fromPlayerToTarget = fromPlayerToTarget.normalized * minDropDistance;
            }

            clampedTargetPosition = transform.position + fromPlayerToTarget;

            if (direction != Vector3.zero)
                aimDirection = direction.normalized;
        }
    }

    void SpawnPlate()
    {

        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        GameObject plate = Instantiate(plateProj, spawnPos, Quaternion.identity);
        Rigidbody prb = plate.GetComponent<Rigidbody>();

        float angleRange = 5f;     //angle of spread

        //create quaternion to rotate in x&y
        Quaternion randomRotation = Quaternion.Euler(Random.Range(-angleRange, angleRange), Random.Range(-angleRange, angleRange), 0);
        Vector3 randomDirection = randomRotation * aimDirection; //randomDirection will combine forward direction with spread

        prb.AddForce(randomDirection * launchForce, ForceMode.Impulse); //addForce to plate in direction with spread

        //play sound clip
        audioSource.PlayOneShot(plateSound);
    }

    void SpawnBigPot()
    {
        if (currentMagic - magicCostBigPot >= 0)
        {
            Vector3 spawnPos = clampedTargetPosition + new Vector3(0, 3f, 0);
            Vector3 IndicatorSpawnPos = clampedTargetPosition;
            IndicatorSpawnPos.y = 0.3f;

            //rotation
            Quaternion rotation = Quaternion.Euler(Random.Range(0, 30), transform.rotation.y, Random.Range(0, 30));

            Instantiate(bigPotPrefab, spawnPos, rotation);
            Instantiate(bigPotIndicatorPrefab, IndicatorSpawnPos, transform.rotation);

            currentMagic -= magicCostBigPot;

            //play sound
            audioSource.PlayOneShot(bigPotSound);
        }
    }

    private IEnumerator SpawnKnifeShield()
    {
        if (currentMagic - magicCostKnifeShield >= 0)
        {
            currentMagic -= magicCostKnifeShield;
            knifeShield.SetActive(true);
            audioSource.PlayOneShot(knifeUnsheath);
            isShieldActive = true;
            yield return new WaitForSeconds(6);
            knifeShield.SetActive(false);
            isShieldActive = false;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        trail.emitting = true;

        rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);

        yield return new WaitForSeconds(dashCooldown);

        trail.emitting = false;
        canDash = true;
    }

    //if player comes into contact with an apple, deduct 5 health
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayerTakeDamage(5);
        }
    }

    //functions to update ammo & magic
    public int GetNumPlates()
    {
        return numPlates;
    }
    public void AddPlates(int amount)
    {
        if (numPlates + amount >= 30)
        {
            numPlates = 30;
        }
        else
        {
            numPlates += amount;
        }
        hasPlates = true;
    }
    public int GetMagic()
    {
        return currentMagic;
    }
    public void DecreaseMagic(int num)
    {
        currentMagic -= num;
    }
    public void IncreaseMagic(int num)
    {
        if (currentMagic == 0)
        {
            hasMagic = true;
        }

        if (currentMagic + num > maxMagic)
        {
            currentMagic = maxMagic;
        }
        else
        {
            currentMagic += num;
        }
    }
    public void PlayerTakeDamage(int num)
    {
        if (healthBar != null)
        {
            currentHealth -= num;
            Instantiate(playerhitEffect, transform.position, Quaternion.identity);
            healthBar.UpdatePlayerHealthBar(maxHealth, currentHealth);    //update health bar when taking damage
        }
        else
        {
            //Debug.LogError("HealthBar is NOT assigned in the Inspector!");
        }
    }
    public void AddHealth(int num)
    {
        if (healthBar != null)
        {
            if (currentHealth + num >= maxHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += num;
            }
            healthBar.UpdatePlayerHealthBar(maxHealth, currentHealth);
        }
    }


    //Create a slow effect for the player after being hit by donut man melee
    public IEnumerator SlowEffect(int num)
    {
        speed -= num;
        yield return new WaitForSeconds(2.5f);
        speed += num;
    }
}

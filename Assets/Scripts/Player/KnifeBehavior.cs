using UnityEngine;

public class KnifeBehavior : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;

    //play sound
    public AudioSource audioSource;
    public AudioClip knifeCutting;

    

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Debug.Log("hit enemy");
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
            BananaBehavior banana = other.GetComponent<BananaBehavior>();
            BananaProjBehavior bananaProj = other.GetComponent<BananaProjBehavior>();
            if (enemy != null)
            {
                enemy.TakeDamage(3);
                enemy.UpdateHealth();
            }
            if (banana != null)
            {
                banana.TakeDamage(3);
                banana.UpdateHealth();
            }
            if (bananaProj != null)
            {
                bananaProj.Die();
            }
            audioSource.PlayOneShot(knifeCutting);
        }
        else if (other.CompareTag("DonutEnemy"))
        {
            DonutManBehavior DonutEnemy = other.GetComponent<DonutManBehavior>();
            if (DonutEnemy != null)
            {
                DonutEnemy.TakeDamage(3);
                audioSource.PlayOneShot(knifeCutting);
            }
        }
    }
}

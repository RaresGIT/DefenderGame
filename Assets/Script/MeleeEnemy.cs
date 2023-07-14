using UnityEngine;
using UnityEngine.TextCore.Text;

public class MeleeEnemy : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public int health;  // Enemy's health
    public int maxHealth = 40;
    public int damage = 10;  // Damage dealt by the enemy
    public float minKnockback = 10f;
    public float maxKnockback = 20f;

    private bool canDealDamage = true;


    public float fireRate = 0.2f;

    FloatingHealthBar healthBar;

    void Awake()
    {
        health = maxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }


    void Start()
    {
        gameObject.tag = "Enemy";  // Make sure the enemy has the correct tag
        healthBar.UpdateHealthBar(health, maxHealth);

    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        //random dumb ai, just walks to the player
        if (player != null)
        {
            if (Vector3.Distance(player.position, gameObject.transform.position) > 1)
            {
                Vector3 direction = player.position - transform.position;
                direction.Normalize();  // Get direction to player

                // Move towards the player
                transform.position += direction * speed * Time.deltaTime;

                // Face the player
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;  // Decrease health by the amount of damage
        healthBar.UpdateHealthBar(health, maxHealth);

        if (health <= 0)  // If the enemy has no health left
        {

            Destroy(gameObject);  // Destroy the enemy
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            MeleeEnemy enemyScript = GetComponent<MeleeEnemy>();

            if (player != null && enemyScript != null && canDealDamage)
            {
                // waits for fire rate for each enemy before starting to register the hit
                player.RegisterEnemyDamage(enemyScript, damage);
                StartCoroutine(WaitFireRate());


                Vector3 direction = (transform.position - collision.transform.position).normalized;
                direction.y = 0;

                float randomKnockbackAmount = Random.Range(minKnockback, maxKnockback);
                Vector3 knockback = direction * randomKnockbackAmount;

                enemyScript.GetComponent<Rigidbody>().AddForce(knockback, ForceMode.Impulse);

            }
        }
    }

    void OnCollisionStay(Collision collision)
    {


    }

    private System.Collections.IEnumerator WaitFireRate()
    {
        canDealDamage = false;
        yield return new WaitForSeconds(fireRate);
        canDealDamage = true;
    }

}

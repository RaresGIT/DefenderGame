using UnityEngine;
using UnityEngine.TextCore.Text;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public int health;  // Enemy's health
    public int maxHealth = 40;
    public int damage = 10;  // Damage dealt by the enemy

    private bool canDealDamage = true;
    public float fireRate = 1f;

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

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            Enemy enemyScript = GetComponent<Enemy>();

            if (player != null && enemyScript != null && canDealDamage)
            {
                // waits for fire rate for each enemy before starting to register the hit
                player.RegisterEnemyDamage(enemyScript, damage);
                StartCoroutine(WaitFireRate());
            }
        }

    }

    private System.Collections.IEnumerator WaitFireRate()
    {
        canDealDamage = false;
        yield return new WaitForSeconds(fireRate);
        canDealDamage = true;
    }

}

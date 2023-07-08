using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera cam;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public int health = 100;  // Player's health
    public int maxHealth = 100;
    public float immunitySeconds = 0.1f;
    private bool canTakeDamage = true;

    private Dictionary<Enemy, int> enemyDamageTracker = new Dictionary<Enemy, int>();  // Track damage from individual enemies


    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            health -= damage;
            StartCoroutine(ApplyImmunity());
            enemyDamageTracker.Clear();
        }


        if (health <= 0)
        {
            Destroy(gameObject);
        }


    }

    public void RegisterEnemyDamage(Enemy enemy, int damage)
    {
        if (enemyDamageTracker.ContainsKey(enemy))
        {
            enemyDamageTracker[enemy] += damage;
        }
        else
        {
            enemyDamageTracker.Add(enemy, damage);
        }

        ApplyEnemyDamage();


    }

    public void UnregisterEnemyDamage(Enemy enemy)
    {
        if (enemyDamageTracker.ContainsKey(enemy))
        {
            enemyDamageTracker.Remove(enemy);
        }

        ApplyEnemyDamage();
    }

    private void ApplyEnemyDamage()
    {
        int totalDamage = 0;
        foreach (var kvp in enemyDamageTracker)
        {
            totalDamage += kvp.Value;
        }

        TakeDamage(totalDamage);

    }

    private System.Collections.IEnumerator ApplyImmunity()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(immunitySeconds);
        canTakeDamage = true;
    }
    void Start()
    {
        cam = Camera.main;

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))  // Assuming max distance of 100 units
            {
                Vector3 targetPosition = hit.point;
                targetPosition.y = transform.position.y;  // Keep the GameObject's original Y position
                Vector3 direction = targetPosition - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);

                toRotation *= Quaternion.Euler(0, 270, 0);  // Add a 90 degree offset on the Y-axis
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1f);  // For smooth rotation
            }
        }

        if (Input.GetMouseButtonDown(1))  // Left click is pressed
        {
            // Spawn a bullet
            Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation * Quaternion.Euler(0, 90, 0));
        }
    }
}
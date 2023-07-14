using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;  // Bullet will be destroyed after this time
    public int damage = 10;  // Damage dealt by the bullet

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))  // The bullet hit an enemy
        {
            MeleeEnemy enemy = collision.gameObject.GetComponent<MeleeEnemy>();
            enemy.TakeDamage(damage);  // Damage the enemy
            Destroy(gameObject);  // Destroy the bullet
        }
    }
}

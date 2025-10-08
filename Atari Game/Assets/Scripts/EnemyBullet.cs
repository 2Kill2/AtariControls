using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        this.gameObject.transform.position += new Vector3(0, -0.1f, 0) * Time.deltaTime * 100;
    }
    
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        // Check if the collided object has a specific tag (e.g., "Enemy")
        if (collision.gameObject.CompareTag("Player"))
        {
            // Access the enemy's health script and apply damage
            PlayerObject enemyHealth = collision.gameObject.GetComponent<PlayerObject>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage();
            }
            Destroy(gameObject);
        }
        
    }
}

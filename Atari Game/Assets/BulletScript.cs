using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += new Vector3(0, 0.1f, 0) * Time.deltaTime * 100;
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        // Check if the collided object has a specific tag (e.g., "Enemy")
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Access the enemy's health script and apply damage
            EnemyFollow enemyHealth = collision.gameObject.GetComponent<EnemyFollow>();
            if (enemyHealth != null)
            {
                enemyHealth.Die();
            }
        }
        Destroy(gameObject);
    }

}

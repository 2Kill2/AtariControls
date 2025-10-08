using JetBrains.Annotations;
using System.Collections;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Profiling;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 3f; // Adjust this value to control enemy speed
    public float diveSpeed = 5f; // Adjust this to control the dive speed
    public int Score = 100; //score for killing this
    private Transform playerTransform;

    public bool shooter = true;

    public bool MoveDown = true;
    public bool Sway = false;
    public bool Sbomb = true;
    public bool right = true;
    int randomInt, randomInt2, randomInt3;
    bool isAlive = true;
    public GameObject manager;

    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject sprite;
    [SerializeField] GameObject projectile;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip charge;
    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip die;

    void Start()
    {
        randomInt = Random.Range(3, 11);
        randomInt2 = Random.Range(1, 5);
        randomInt3 = Random.Range(5, 20);
        // Find the player game object by its tag (ensure your player has the "Player" tag)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.Find("GameManager");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player game object with 'Player' tag not found!");
        }
        StartCoroutine(DelayAction2());
        StartCoroutine(SpawnRoutine4());
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        // Check if the collided object has a specific tag (e.g., "Enemy")
        if (collision.gameObject.CompareTag("Player") && isAlive == true)
        {
            // Access the enemy's health script and apply damage
            PlayerObject enemyHealth = collision.gameObject.GetComponent<PlayerObject>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage();
            }
            Die(false);
        }
    }

    void Update()
    {
        if (MoveDown)
        {
            this.gameObject.transform.position += new Vector3(0f, -0.1f, 0) * Time.deltaTime * speed;
            if (this.gameObject.transform.position.y < randomInt2)
            {
                Sway = true;
                MoveDown = false;
            }
        }

        if (playerTransform != null && Sbomb == true)
        {
            // Calculate the direction towards the player
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, diveSpeed * Time.deltaTime);
        }

        if (Sway == true)
        {
            if (this.gameObject.transform.position.x > 7)
            {
                right = false;
            }
            if (this.gameObject.transform.position.x < -7)
            {
                right = true;
            }
            if (right)
            {
                this.gameObject.transform.position += new Vector3(0.1f, 0, 0) * Time.deltaTime * speed;
            }
            if (!right)
            {
                this.gameObject.transform.position += new Vector3(-0.1f, 0, 0) * Time.deltaTime * speed;
            }

        }

    }

    public void Die(bool honor)
    {
        if (isAlive == true)
        {
            particle.gameObject.SetActive(true);
            isAlive = false;
            MoveDown = false;
            Sway = false;
            Sbomb = false;
            audioSource.PlayOneShot(die);
            sprite.SetActive(false);
            GameManager gameMan = manager.gameObject.GetComponent<GameManager>();
            if (honor == true)
            {
                gameMan.AddScore(Score);
            }
            else
            {
                gameMan.AddScore(10);
            }

            particle.Emit(10);
            StartCoroutine(DelayAction());
        }
    }

    IEnumerator SpawnRoutine4()
    {
        while (shooter) // Loop indefinitely
        {
            randomInt = Random.Range(1, 14);
            //Debug.Log(randomInt);
            
            yield return new WaitForSeconds(randomInt); // Wait for the specified interval
            Shoot(); // Spawn at spawner's position
        }
    }

    private void Shoot()
    {
        if (isAlive==true)
        {
            GameObject newObject = Instantiate(projectile, transform.position, Quaternion.identity);
            audioSource.PlayOneShot(charge);
        }
    }

    IEnumerator DelayAction2()
    {
        yield return new WaitForSeconds(randomInt3);
        if (isAlive == true)
        {
            Kamikaze();
        }
    }

    IEnumerator DelayAction()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    public void Kamikaze()
    {
        Sway = false;
        Sbomb = true;
        audioSource.PlayOneShot(charge);
    }
}
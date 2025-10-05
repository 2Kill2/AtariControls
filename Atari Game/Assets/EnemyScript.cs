using UnityEngine;
using System.Collections;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 3f; // Adjust this value to control enemy speed
    public float diveSpeed = 5f; // Adjust this to control the dive speed
    private Transform playerTransform;

    public bool MoveDown = true;
    public bool Sway = false;
    public bool Sbomb = true;
    public bool right = true;
    int randomInt, randomInt2, randomInt3;
    bool isAlive = true;

    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject sprite;

    void Start()
    {
        randomInt = Random.Range(3, 11);
        randomInt2 = Random.Range(1, 5);
        randomInt3 = Random.Range(5, 20);
        // Find the player game object by its tag (ensure your player has the "Player" tag)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player game object with 'Player' tag not found!");
        }
        StartCoroutine(DelayAction2());
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

    public void Die()
    {
        if (isAlive == true)
        {
            particle.gameObject.SetActive(true);
            isAlive = false;
            MoveDown = false;
            Sway = false;
            Sbomb = false;
            sprite.SetActive(false);
            particle.Emit(10);
            StartCoroutine(DelayAction());
        }
    }

    IEnumerator DelayAction2()
    {
        yield return new WaitForSeconds(randomInt3);
        Kamikaze();
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
    }
}
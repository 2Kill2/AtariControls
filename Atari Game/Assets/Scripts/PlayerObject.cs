using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class PlayerObject : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject projectile2;
    [SerializeField] GameObject selfSprite;
    [SerializeField] GameObject shieldSprite;
    [SerializeField] GameObject manager;

    //lol
    [SerializeField] Image h1;
    [SerializeField] Image h2;
    [SerializeField] Image h3;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip charge;
    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip die;


    private bool fired = true;
    private bool shield = false;
    private bool cd = false;
    private int HP = 3;
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        //move left and right
        if (Input.GetKey(KeyCode.A) || horizontalInput < 0)
        {
            if (this.gameObject.transform.position.x >= -7)
            {
                this.gameObject.transform.position += new Vector3(-0.1f, 0, 0) * Time.deltaTime * 100;
            }
        }

        if (Input.GetKey(KeyCode.D) || horizontalInput > 0)
        {
            if (this.gameObject.transform.position.x <= 7)
            {
                this.gameObject.transform.position += new Vector3(0.1f, 0, 0) * Time.deltaTime * 100;
            }
        }

        if (Input.GetKey(KeyCode.W) || vertInput > 0)
        {
            if (fired == true && shield == false)
            {
                ShootPOW();
            }
        }

        if (Input.GetKey(KeyCode.S) || vertInput < 0)
        {
            shield = true;
            shieldSprite.SetActive(true);
        }
        else
        {
            shield = false;
            shieldSprite.SetActive(false);
        }

        //attack
        if (Gamepad.current != null)
        {
            // Check if the 'A' button (or equivalent) is pressed
            if (Gamepad.current[GamepadButton.A].isPressed)
            {
                if (fired == true && shield == false)
                {
                    Shoot();
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (fired == true && shield == false)
                {
                    Shoot();
                }
            }
        }
    }

    private void Shoot()
    {
        fired = false;
        GameObject newObject = Instantiate(projectile, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(shoot);
        StartCoroutine(DelayAction(1));
    }

    private void ShootPOW()
    {
        fired = false;
        GameObject newObject = Instantiate(projectile2, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(shoot);
        StartCoroutine(DelayAction(3));
    }

    private void DamageTaking()
    {
        cd = true;
        audioSource.PlayOneShot(die);
        if (HP == 2)
        {
            h3.gameObject.SetActive(false);
        }
        else if (HP == 1)
        {
            h2.gameObject.SetActive(false);
        }
        else if (HP == 0)
        {
            h1.gameObject.SetActive(false);
        }
        StartCoroutine(DelayAction2());
    }

    public void TakeDamage()
    {
        if (shield == false && cd == false)
        {
            HP -= 1;
            DamageTaking();
        }
        if (HP == 0)
        {
            GameManager enemyHealth = manager.gameObject.GetComponent<GameManager>();
            if (enemyHealth != null)
            {
                enemyHealth.GameOver();
            }
        }
    }

    IEnumerator DelayAction(int secs)
    {
        fired = false;
        yield return new WaitForSeconds(secs);
        fired = true;
    }

    IEnumerator DelayAction2()
    {
        for (int i = 0; i < 3; i++)
        {
            selfSprite.SetActive(false);
            yield return new WaitForSeconds(0.25f);

            selfSprite.SetActive(true);
            yield return new WaitForSeconds(0.25f);
        }
        Debug.Log("cd ended");
        cd = false;
    }
}

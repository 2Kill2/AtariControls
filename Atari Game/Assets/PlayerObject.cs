using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerObject : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    private bool fired = true;
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
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
        //attack
        if (Gamepad.current != null)
        {
            // Check if the 'A' button (or equivalent) is pressed
            if (Gamepad.current[GamepadButton.A].isPressed)
            {
                if (fired == true) 
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
        StartCoroutine(DelayAction());
    }

    IEnumerator DelayAction()
    {
        fired = false;
        yield return new WaitForSeconds(1);
        fired = true;
    }
}

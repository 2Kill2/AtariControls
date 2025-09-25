using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.gameObject.transform.position += new Vector3(0, 0.01f, 0) * Time.deltaTime * 1000;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.position += new Vector3(-0.01f, 0, 0) * Time.deltaTime * 1000;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.position += new Vector3(0, -0.01f, 0) * Time.deltaTime * 1000;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.position += new Vector3(0.01f, 0, 0) * Time.deltaTime * 1000;
        }
    }
}

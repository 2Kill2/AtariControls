using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject wasp;
    public bool GameActive = true;
    int randomInt;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (GameActive) // Loop indefinitely
        {
            randomInt = Random.Range(-7, 8);
            //Debug.Log(randomInt);
            Instantiate(wasp, transform.position = new Vector3(randomInt, 6, 0), Quaternion.identity); // Spawn at spawner's position
            yield return new WaitForSeconds(3); // Wait for the specified interval
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

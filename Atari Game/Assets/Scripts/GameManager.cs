using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject wasp;
    [SerializeField] GameObject wasp2;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI scoreUI;
    [SerializeField] TextMeshProUGUI gameOverScore;
    private GameObject activeEnemy;
    public bool GameActive = true;
    public int CurrentScore = 0;
    int randomInt, randomInt2;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.SetActive(true);
        StartCoroutine(SpawnRoutine());
    }

    public void AddScore(int score)
    {
        CurrentScore += score;
    }

    public void GameOver()
    {
        DestroyAllWithTag("Enemy");
        GameActive = false;
        gameOverScore.text = scoreUI.text;
        gameOver.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    void DestroyAllWithTag(string tagToDestroy)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tagToDestroy);

        foreach (GameObject obj in gameObjects)
        {
            Destroy(obj);
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (GameActive) // Loop indefinitely
        {
            randomInt = Random.Range(-7, 8);
            randomInt2 = Random.Range(1, 3);
            if (randomInt2 == 1)
            {
                activeEnemy = wasp;
            }
            else if (randomInt2 == 2)
            {
                activeEnemy = wasp2;
            }
            Instantiate(activeEnemy, transform.position = new Vector3(randomInt, 6, 0), Quaternion.identity); // Spawn at spawner's position
            yield return new WaitForSeconds(1); // Wait for the specified interval
        }
    }

    // Update is called once per frame
    void Update()
    {
        string m = CurrentScore.ToString();
        scoreUI.text = m;
    }
}

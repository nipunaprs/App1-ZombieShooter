using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int enemiesAlive = 0;

    public int round = 0;

    public GameObject[] spawnPoints;

    public GameObject enemyPrefab;

    public GameObject player;

    public Text roundNumber;
    public Text enemiesLeft;
    public Text playerHealth;
    public Text endGameRoundSurvived;

    public GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth.text = "Health " + player.GetComponent<PlayerManager>().health.ToString();
        enemiesLeft.text = "Enemies " + enemiesAlive.ToString();

        if (enemiesAlive == 0)
        {
            round++;
            NextWave(round);
            roundNumber.text = "Round " + round.ToString();
        }
    }

    public void NextWave(int round)
    {

        for(var x=0; x < round; x++)
        {
            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; //selects a random spawn point from array

            GameObject enemySpawned = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            enemySpawned.GetComponent<ZombieManager>().gameManager = GetComponent<GameManager>();
            enemiesAlive++;
        }

        
    }

    public void Restart()
    {
        Time.timeScale = 1; //Reset time movement
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1; //Reset time movement
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        
        Time.timeScale = 0; //Pause time code
        Cursor.lockState = CursorLockMode.None; //Allow cursor use again
        endScreen.SetActive(true);
        endGameRoundSurvived.text = round.ToString();
    }

}

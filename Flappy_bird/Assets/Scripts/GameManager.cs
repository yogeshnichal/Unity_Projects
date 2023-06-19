/*================================================================================
Project Created By: Yogesh Prabhu Nichal.
Github: https://github.com/yogeshnichal/Unity_Projects
=================================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public bool isGameActive;

    // UI
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highestScoreText;
    public GameObject gameOverScreen; // gameOverText and restartButton
    public GameObject titleScreen; // titleText and startButton

    float spawnDelay = 1;
    float spawnInterval = 1.5f;
    float xSpawnPos = 20;
    float zSpawnPos = -2;
    [SerializeField] float ySpawnMin;
    [SerializeField] float ySpawnMax;

    int score;
    int highestScore;
    int scoreIncrement = 100; // 100 points for each second survived

    public void StartGame() {
        isGameActive = true;
        titleScreen.SetActive(false);
        scoreText.gameObject.SetActive(true);
        GameObject.Find("Player").GetComponent<Rigidbody>().useGravity = true;
        InvokeRepeating("SpawnObject", spawnDelay, spawnInterval);
        InvokeRepeating("UpdateScore", 0, 1);
    }

    public void RestartGame() {
        // reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndGame() {
        isGameActive = false;
        highestScore = LoadScore();
        if (score > highestScore) {
            SaveScore();
            highestScore = score;
        }
        highestScoreText.text = "Highest Score: " + highestScore;
        gameOverScreen.gameObject.SetActive(true);
    }

    void SpawnObject() {
        if (!isGameActive) {
            return;
        }
        float ySpawnPos = Random.Range(ySpawnMin, ySpawnMax);
        // first flip a coin to determine if the object comes from the top or bottom
        bool bottom  = (Random.value > 0.5f);
        if (bottom) { // negate y
            ySpawnPos = -ySpawnPos;
            Quaternion spawnRotation = Quaternion.Euler(0, 0, 0);
            obstaclePrefab.transform.GetChild(0).gameObject.transform.rotation = spawnRotation;
        } else { // rorate z by 180
            Quaternion spawnRotation = Quaternion.Euler(0, 0, 180);
            obstaclePrefab.transform.GetChild(0).gameObject.transform.rotation = spawnRotation;
        }
        Vector3 spawnPos = new Vector3(xSpawnPos, ySpawnPos, zSpawnPos);
        Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
    }

    void UpdateScore() {
        if (!isGameActive) {
            return;
        }
        score += scoreIncrement;
        scoreText.text = "Score: " + score;
    }

    void SaveScore() {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }

    int LoadScore() {
        return PlayerPrefs.GetInt("Score", 0);
    }
}

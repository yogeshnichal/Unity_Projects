/*================================================================================
Project Created By: Yogesh Prabhu Nichal.
Github: https://github.com/yogeshnichal/Unity_Projects
=================================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] float speed;
    float leftBound = -55;
    PlayerController playerController;
    GameManager gameManager;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!gameManager.isGameActive || playerController.isGameOver) {
            return;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x < leftBound && !gameObject.CompareTag("Background")) {
            Destroy(gameObject);
        }
    }
}

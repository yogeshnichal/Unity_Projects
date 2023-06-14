/*================================================================================
Project Created By: Yogesh Prabhu Nichal.
Github: https://github.com/yogeshnichal/Unity_Projects
=================================================================================*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public SpawnFood sf;
    public Snake snake;

    public Toggle toggle;

    public Text textLose;

    public Flash flash;

	void Update () {

        if (Input.GetKeyUp(KeyCode.Space))
        {
            sf.Continuous = toggle.isOn;
            
            sf.enabled = true;
            snake.enabled = true;
            snake.gameObject.SetActive(true);

            textLose.gameObject.SetActive(false);
            toggle.gameObject.SetActive(false);
            flash.Stop();

            //侦听
            snake.OnLose += OnLose;
        }
	
	}

    void OnLose()
    {
        textLose.text = "You lose.";
        textLose.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}

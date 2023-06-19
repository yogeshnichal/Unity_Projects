using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// level management
public class SceneContral : MonoBehaviour
{
    #region Level Settings
    //level title
    public Text sceneLevel;
    //level number
    private int levelNumber;
    private Scene currentScene; // current Scene
                                //Total number of levels
    private int allScenesNumber;
    #endregion

    #region button
    //Reference of the previous level and the next level Button
    public Button Btn_Next;
    public Button Btn_Last;
    #endregion

    #region Component UI
    //dialogue
    public Image tipWindow;
    //Text component for step detection
    public Text walkCountText;
    //steps taken
    public int walkCount = 0;
    //Game win
    public Image WinImage;
    #endregion

    /// <summary>
    /// SceneContral single instance
    /// </summary>
    private static SceneContral instance;
    public static SceneContral Instance
    {
        get
        {
            return SceneContral.instance;
        }
    }
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (WinImage != null)
        {
            //The game victory picture is not visible
            WinImage.gameObject.SetActive(false);
        }
        //Get the current Scene
        currentScene = SceneManager.GetActiveScene();
        //Get the number of the current Scene
        levelNumber = currentScene.buildIndex;
        //Scene total
        allScenesNumber = SceneManager.sceneCountInBuildSettings;
        // set to invisible
        tipWindow.gameObject.SetActive(false);
        // Get the current level number
        sceneLevel.text = "No" + levelNumber + "Off";
    }
    void Update()
    {
        walkCountText.text = "Steps\n" + walkCount.ToString();
    }
    /// <summary>
    /// Click to load the main menu
    /// </summary>
    public void ClickMenu()
    {
        //load the main menu
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// Skip to previous level
    /// </summary>
    public void ClickLastScene()
    {
        if (levelNumber == 1)
        {
             // Get the content of the dialog box Text component
             Text txtContent = tipWindow.transform.GetChild(0).GetComponent<Text>();
            txtContent.text = "It's already the first level!";
            tipWindow.gameObject.SetActive(true);
        }
        else
        {
            //Number of levels--
            levelNumber--;
            SceneManager.LoadScene(levelNumber);
        }
    }


    public void ClickNextScene()
    {
        if (levelNumber + 1 >= allScenesNumber)
        {
             // Get the content of the dialog box Text component
             Text txtContent = tipWindow.transform.GetChild(0).GetComponent<Text>();
            txtContent.text = "It's the last level!";
            tipWindow.gameObject.SetActive(true);
        }
        else
        {
            //Number of levels++
            levelNumber++;
            SceneManager.LoadScene(levelNumber);
        }

    }
    /// <summary>
    /// Reset the current level
    /// </summary>
    public void ClickRestart()
    {
        // reset the current game
        SceneManager.LoadScene(levelNumber);
    }
    /// <summary>
    /// Cancel button, making tipWindow invisible
    /// </summary>
    public void ClickCancel()
    {
        if (tipWindow.enabled)
        {
            tipWindow.gameObject.SetActive(false);
        }
    }
}
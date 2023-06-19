using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// start interface
public class StartScene : MonoBehaviour
{
    //Get the logo
    private Text txtLogo;
    //The font size of the Logo
    private int logoSize;
    //Get tipWindow
    public Image tipWindow;
    void Start()
    {
        //Get the Text component on the Logo
        txtLogo = transform.Find("Logo").gameObject.GetComponent<Text>();
        // get font size
        logoSize = txtLogo.fontSize;
        //When starting the game, make tipWindow invisible
        tipWindow.gameObject.SetActive(false);
    }
    void Update()
    {
        EffectLogo();
    }
    /// <summary>
    /// font effects
    /// </summary>
    void EffectLogo()
    {
        //Make the font size change between 220-250
        txtLogo.fontSize = (int)Mathf.PingPong(Time.time * 220 / 3, 30) + 220;
    }

    /// <summary>
    /// Start the game
    /// </summary>
    public void ClickStart()
    {
        //load the first scene
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// Game Introduction
    /// </summary>
    public void ClickAbout()
    {
        //Game introduction
        // set visible
        tipWindow.gameObject.SetActive(true);
    }
    /// <summary>
    /// exit the game
    /// </summary>
    public void ClickQuit()
    {
        //exit the game
        Application.Quit();
    }
    /// <summary>
    /// Click to cancel
    /// </summary>
    public void ClickCancel()
    {
        // set invisible
        tipWindow.gameObject.SetActive(false);
    }
}
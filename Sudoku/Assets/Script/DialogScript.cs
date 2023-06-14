/*================================================================================
Project Created By: Yogesh Prabhu Nichal.
Github: https://github.com/yogeshnichal/Unity_Projects
=================================================================================*/

using System;
using UnityEngine;

public class DialogScript : MonoBehaviour
{
    public static bool Displaying;

    private void Awake()
    {
        Displaying = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
            Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Displaying = false;
    }
}

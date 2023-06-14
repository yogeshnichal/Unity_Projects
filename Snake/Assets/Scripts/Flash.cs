/*================================================================================
Project Created By: Yogesh Prabhu Nichal.
Github: https://github.com/yogeshnichal/Unity_Projects
=================================================================================*/

using UnityEngine;
using System.Collections;

public class Flash : MonoBehaviour {

    public float interval;

    void Start()
    {
        InvokeRepeating("FlashLabel", 0, interval);
    }

    void FlashLabel()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }

    public void Stop()
    {
        gameObject.SetActive(false);
        CancelInvoke("FlashLabel");
    }
}

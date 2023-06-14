/*================================================================================
Project Created By: Yogesh Prabhu Nichal.
Github: https://github.com/yogeshnichal/Unity_Projects
=================================================================================*/

using System;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static float Volume = 1f;
    public AudioSource Soruce;
    public static int Missing;
    public static Sprite Background;

    private void Update()
    {
        if(Soruce != null)
            Soruce.volume = Volume;
    }

    public void VolumeChange(Single volume)
    {
        Volume = volume;
    }
    
}

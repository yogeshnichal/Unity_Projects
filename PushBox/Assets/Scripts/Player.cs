using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Role movement and detection
public class Player : MonoBehaviour
{
    //Define a Map class
    private MapCreator BoxMap;
    // total number of scenes
    private int allSceneCount;
    //steps taken
    public int walkCount = 0;
    // whether to win
    private bool isWin = false;

    void Start()
    {
        //Total number of loaded scenes
        allSceneCount = SceneManager.sceneCountInBuildSettings;
        //Get the loaded activation object of type MapCreator
        BoxMap = FindObjectOfType<MapCreator>();
    }
    void Update()
    {
        Move();
        if (isWin)
        {
            //Display the picture of the game victory
            SceneContral.Instance.WinImage.gameObject.SetActive(true);
        }
        isWin = false;
    }

    /// <summary>
    /// The player moves and detects
    /// </summary>
    void Move()
    {
        int moveX = 0;
        int moveZ = 0;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveX++;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveX--;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveZ++;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveZ--;
        }
        //player's next position
        int nextX = moveX + (int)transform.position.x;
        int nextZ = moveZ + (int)transform.position.z;
         // Determine whether the position is a wall
         if (IsWall(nextX, nextZ)) return;
        //Judge if the next position is a box
        if (IsBox(nextX, nextZ))
        {
            // get the next position
            int nextNextX = nextX + moveX;
            int nextNextZ = nextZ + moveZ;
            //Judge whether the next position is a wall or a box
            if (IsBox(nextNextX, nextNextZ) || IsWall(nextNextX, nextNextZ)) return;
            GameObject box = GetBox(nextX, nextZ);
            box.transform.position = new Vector3(nextNextX, 0.5f, nextNextZ);
            //update structure
            BoxMap.GetPosBoxMap().Remove(BoxMap.TwoDToOneD(nextX, nextZ));
            BoxMap.GetPosBoxMap().Add(BoxMap.TwoDToOneD(nextNextX, nextNextZ), box);
        }
        if (isMove(moveX, moveZ))
        {
            //player moves to next position
            transform.position = new Vector3(nextX, 0.5f, nextZ);
            SceneContral.Instance.walkCount++;
        }
        CheckWin();
    }
    /// <summary>
    /// According to x, z, coordinates to determine whether it is a wall
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    bool IsWall(int x, int z)
    {
        return BoxMap.GetWallPosSet().Contains(BoxMap.TwoDToOneD(x, z));
    }
    /// <summary>
    /// According to x, z, coordinates to determine whether it is a box
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    bool IsBox(int x, int z)
    {
        return BoxMap.GetPosBoxMap().ContainsKey(BoxMap.TwoDToOneD(x, z));
    }
    /// <summary>
    /// Get the GameObject of the box according to the x, y position
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    GameObject GetBox(int x, int z)
    {
        return BoxMap.GetPosBoxMap()[BoxMap.TwoDToOneD(x, z)];
    }
    /// <summary>
    /// Determine if the player is moving
    /// </summary>
    /// <param name="x"> horizontal offset</param>
    /// <param name="z"> Hammer offset</param>
    /// <returns></returns>
    bool isMove(int x, int z)
    {
        if (x == 0 && z == 0)
        {
            return false;
        }
        else return true;
    }
    /// <summary>
    /// Check if the game is won
    /// </summary>
    void CheckWin()
    {
        int num = 0;
        //Traverse the HashSet of the target point
        foreach (var tar_pos in BoxMap.GetTargetPosList())
        {
            //If the position of the target point coincides with the position of the box
            if (BoxMap.GetPosBoxMap().ContainsKey(tar_pos)) ++num;
        }
        if (num == BoxMap.GetTargetPosList().Count)
        {
            //load the next scene
            /*
             * 1. Get the current scene
             * 2. Get the index of the current scene
             * 3. If the next scene exists, load the scene
             */
            Scene scene = SceneManager.GetActiveScene();
            int sceneCount = scene.buildIndex;
            if (sceneCount == allSceneCount - 1)
            {
                //Game win
                isWin = true;
            }
            if (sceneCount + 1 < allSceneCount)
            {
                SceneManager.LoadScene(sceneCount + 1);
            }
        }
    }
}
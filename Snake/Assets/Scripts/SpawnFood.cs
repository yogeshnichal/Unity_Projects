/*================================================================================
Project Created By: Yogesh Prabhu Nichal.
Github: https://github.com/yogeshnichal/Unity_Projects
=================================================================================*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnFood : MonoBehaviour
{
    // Food Prefab
    public GameObject foodPrefab;

    // Borders
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    public bool Continuous = true;

    void Start()
    {
        // Spawns a food every 4 seconds
        InvokeRepeating("Spawn", 3, 4);
    }

    // Spawn one piece of food
    void Spawn()
    {
        if (Continuous)
        {
            // x position between left & right border
            int x = (int)Random.Range(borderLeft.position.x,
                                      borderRight.position.x);

            // y position between top & bottom border
            int y = (int)Random.Range(borderBottom.position.y,
                                      borderTop.position.y);

            // Instantiate the food at (x, y)
            Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity); // default rotation
        }
        else
        {
            GameObject food = GameObject.Find("food(Clone)");
            if (food == null)
            {
                // x position between left & right border
                int x = (int)Random.Range(borderLeft.position.x,
                                          borderRight.position.x);

                // y position between top & bottom border
                int y = (int)Random.Range(borderBottom.position.y,
                                          borderTop.position.y);

                // Instantiate the food at (x, y)
                Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity); // default rotation
            }
        }
    }
}

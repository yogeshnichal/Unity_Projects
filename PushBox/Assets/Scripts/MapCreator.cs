using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Create a map
public class MapCreator : MonoBehaviour
{
    // input map
    public string[] map;
    //All prefabs 0 Player, 1 Box, 2 Wall, 3 Target
    //public List<GameObject> allPrefabs = new List<GameObject>();
    public GameObject[] allPrefabs;
    //Wall and target point position
    private Dictionary<int, GameObject> pos_box_map; //box position
    private HashSet<int> wall_pos_set; //The position of the wall
    private List<int> tar_pos_list; //target point position
                                    //Use 2D to 1D
    public const int MAPSIZE = 100;
    //The starting point of building the map
    public int left_top_x = -4;
    public int left_top_z = -4;
    public float left_top_y = 0.5f;

    private void Awake()
    {
        //Initialize the data structure
        pos_box_map = new Dictionary<int, GameObject>();
        wall_pos_set = new HashSet<int>();
        tar_pos_list = new List<int>();
        //Application.Quit() is only useful on the platform, not in the unity3d simulator
    }

    void Start()
    {
        //Construct maps from left to right, top to bottom
        int row_pos = left_top_x;
        foreach (var row in map)
        {
            int col_pos = left_top_z;
            for (int i = 0; i < row.Length; i++)
            {
                Vector3 cell_pos = new Vector3(row_pos, 0.5f, col_pos);
                //Instantiate the corresponding prefab and store it in the data structure
                if (row[i] == '1') // wall
                {
                    Instantiate(allPrefabs[2], cell_pos, Quaternion.identity);
                    wall_pos_set.Add(TwoDToOneD(row_pos, col_pos));
                }
                else if (row[i] == '2')//player
                {
                    Instantiate(allPrefabs[0], cell_pos, Quaternion.identity);
                }
                else if (row[i] == '3')//box
                {
                    GameObject newBox = Instantiate(allPrefabs[1], cell_pos, Quaternion.identity);
                    pos_box_map.Add(TwoDToOneD(row_pos, col_pos), newBox);
                }
                else if (row[i] == '4') //target point
                {
                    Instantiate(allPrefabs[3], cell_pos, Quaternion.identity);
                    tar_pos_list.Add(TwoDToOneD(row_pos, col_pos));
                }
                col_pos++;
            }
            row_pos++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Returns the data structure storing the box
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, GameObject> GetPosBoxMap()
    {
        return pos_box_map;
    }
    /// <summary>
    /// Returns the data structure of the storage wall
    /// </summary>
    /// <returns></returns>
    public HashSet<int> GetWallPosSet()
    {
        return wall_pos_set;
    }
    /// <summary>
    /// Returns the data structure storing the target point
    /// </summary>
    /// <returns></returns>
    public List<int> GetTargetPosList()
    {
        return tar_pos_list;
    }
    /// <summary>
    /// Convert the X, Z coordinates into a number, so that each coordinate corresponds to a number,
    /// stored in the data structure, and convenient to access the value in the data structure through the coordinates
    /// </summary>
    /// <param name="x"> Coordinate X</param>
    /// <param name="z"> Coordinate Z</param>
    /// <returns></returns>
    public int TwoDToOneD(int x, int z)
    {
        return MAPSIZE * x + z;
    }
}
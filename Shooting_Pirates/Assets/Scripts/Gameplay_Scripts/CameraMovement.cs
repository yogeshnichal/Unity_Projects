using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform playerTraget;

    [SerializeField]
    private float smoothSpeed = 2f;

    [SerializeField]
    private float plyerBoundMin_Y = -1f, playerBoundMin_X = -65f, playerBoundMax_X = 65f;

    [SerializeField]
    private float Y_Gap = 2f;

    private Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        playerTraget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerTraget)
            return;
        tempPos = transform.position;

        if(playerTraget.position.y <= plyerBoundMin_Y)
        {
            tempPos = Vector3.Lerp(transform.position, new Vector3(playerTraget.position.x, playerTraget.position.y, -10f ), Time.deltaTime * smoothSpeed);
        }
        else
        {
            tempPos = Vector3.Lerp(transform.position, new Vector3(playerTraget.position.x, playerTraget.position.y + Y_Gap, -10f), Time.deltaTime * smoothSpeed);
        }

        if(tempPos.x > playerBoundMax_X)
            tempPos.x = playerBoundMax_X;

        if (tempPos.x < playerBoundMin_X)
            tempPos.x = playerBoundMin_X;

        transform.position = tempPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{

    public Transform player;

    void LateUpdate()
    {
        //MiniMap following the player
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        //MiniMaprotating with the Player
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

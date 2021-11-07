using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room 
{ 

    public Vector2 location;

    public GameObject room;

    MapSystem mapSystem;

    public bool left = false;
    public bool right = false;
    public bool up = false;
    public bool down = false;

    public Room() { }
    public Room(Vector2 v)
    {
        location = new Vector2(v.x, v.y); 
        //Instantiate(rooms[0], currentPosition, Quaternion.identity);
    }
    public Room(float x, float y)
    {
        location = new Vector2(x, y);
    }
    public void Destroy()
    {

    }

    void Update()
    {
        Debug.Log("updated");
        mapSystem = GameObject.Find("MapSystem").GetComponent<MapSystem>();
        for (int i = 0; i < mapSystem.roomList.Count; i++)
        {
            if (mapSystem.roomList[i].location.x == location.x)
            {
                if (mapSystem.roomList[i].location.y == location.y + 20)
                {
                    up = true;
                }
                else if (mapSystem.roomList[i].location.y == location.y - 20)
                {
                    down = true;
                }
            }
            if (mapSystem.roomList[i].location.y == location.y)
            {
                if (mapSystem.roomList[i].location.x == location.x + 32)
                {
                    right = true;
                }
                else if (mapSystem.roomList[i].location.x == location.x - 32)
                {
                    left = true;
                }
            }
        }
    }
}

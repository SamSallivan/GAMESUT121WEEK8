using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MapSystem : MonoBehaviour
{

    public enum SeedType { RANDOM, CUSTOM }
    [Header("Random Related Stuff")]
    public SeedType seedType = SeedType.RANDOM;
    System.Random random;
    public int seed = 0;
    public float cd = 0;

    public List<GameObject> rooms;

    [Space]
    private bool animatedPath = false;
    public List<Room> roomList = new List<Room>();
    public int pathLength = 10;
    [Range(1.0f, 10.0f)]
    public Vector2 roomSize = new Vector2(32.0f, 20.0f);

    public Transform startLocation;

    // Start is called before the first frame update
    void Start()
    {
        SetSeed();

        CreatePath();

    }

    void SetSeed()
    {
        if (seedType == SeedType.RANDOM)
        {
            random = new System.Random();
        }
        else if (seedType == SeedType.CUSTOM)
        {
            random = new System.Random(seed);
        }
    }

    void CreatePath()
    {

        roomList.Clear();

        for (int i = 0; i < rooms.Count; i++)
        {
            Destroy(rooms[i].gameObject);
        }

        rooms.Clear();

        Vector2 currentPosition = startLocation.transform.position;
        roomList.Add(new Room(currentPosition));

        Vector2 tempPosition = startLocation.transform.position;
        //int count = 0;

        for (int i = 0; i < pathLength; i++)
        {
            bool overlap = false;
            int count = 0;
            do
            {
                count++;
                overlap = false;
                int n = random.Next(100);

                if (n.IsBetween(0, 33))
                {
                    tempPosition = new Vector2(currentPosition.x + roomSize.x, currentPosition.y);
                }
                else if (n.IsBetween(34, 66))
                {
                    tempPosition = new Vector2(currentPosition.x - roomSize.x, currentPosition.y);
                }
                else if (n.IsBetween(67, 88))
                {
                    tempPosition = new Vector2(currentPosition.x, currentPosition.y + roomSize.y);
                }
                else if (n.IsBetween(89, 99))
                {
                    tempPosition = new Vector2(currentPosition.x, currentPosition.y - roomSize.y);
                }

                for (int j = 0; j < roomList.Count; j++)
                {
                    if (roomList[j].location == tempPosition)
                    {
                        overlap = true;
                        Debug.Log("overlapped");
                    }
                }
            } while (overlap && count <= 16);

            currentPosition = tempPosition;
            //overlap = false;
            roomList.Add(new Room(currentPosition));
            Debug.Log("room list created");

        }

        for (int i = 0; i < roomList.Count; i++)
        {

            for (int j = 0; j < roomList.Count; j++)
            {
                if (roomList[j].location.x == roomList[i].location.x)
                {
                    if (roomList[j].location.y == roomList[i].location.y + 20)
                    {
                        roomList[i].up = true;
                    }
                    else if (roomList[j].location.y == roomList[i].location.y - 20)
                    {
                        roomList[i].down = true;
                    }
                }
                if (roomList[j].location.y == roomList[i].location.y)
                {
                    if (roomList[j].location.x == roomList[i].location.x + 32)
                    {
                        roomList[i].right = true;
                    }
                    else if (roomList[j].location.x == roomList[i].location.x - 32)
                    {
                        roomList[i].left = true;
                    }
                }
            }

            GameObject room = null;

            if (roomList[i].left && roomList[i].up && roomList[i].right && roomList[i].down)
            {
                room = Instantiate(GameObject.Find("Room!"), roomList[i].location, Quaternion.identity);
            }
            else if (roomList[i].left && roomList[i].up && roomList[i].right && !roomList[i].down)
            {
                room = Instantiate(GameObject.Find("Room!Down"), roomList[i].location, Quaternion.identity);
            }
            else if (roomList[i].left && roomList[i].up && !roomList[i].right && roomList[i].down)
            {
                room = Instantiate(GameObject.Find("Room!Right"), roomList[i].location, Quaternion.identity);
            }
            else if (roomList[i].left && !roomList[i].up && roomList[i].right && roomList[i].down)
            {
                room = Instantiate(GameObject.Find("Room!Up"), roomList[i].location, Quaternion.identity);
            }
            else if (!roomList[i].left && roomList[i].up && roomList[i].right && roomList[i].down)
            {
                room = Instantiate(GameObject.Find("Room!Left"), roomList[i].location, Quaternion.identity);
            }
            else if (roomList[i].left && roomList[i].up && !roomList[i].right && !roomList[i].down)
            {
                room = Instantiate(GameObject.Find("RoomLeftUp"), roomList[i].location, Quaternion.identity);
            }
            else if (roomList[i].left && !roomList[i].up && roomList[i].right && !roomList[i].down)
            {
                room = Instantiate(GameObject.Find("RoomLeftRight"), roomList[i].location, Quaternion.identity);
            }
            else if (!roomList[i].left && roomList[i].up && roomList[i].right && !roomList[i].down)
            {
                room = Instantiate(GameObject.Find("RoomUpRight"), roomList[i].location, Quaternion.identity);
            }
            else if (roomList[i].left && !roomList[i].up && !roomList[i].right && roomList[i].down)
            {
                room = Instantiate(GameObject.Find("RoomLeftDown"), roomList[i].location, Quaternion.identity);
            }
            else if (!roomList[i].left && roomList[i].up && !roomList[i].right && roomList[i].down)
            {
                room = Instantiate(GameObject.Find("RoomUpDown"), roomList[i].location, Quaternion.identity);
            }
            else if (!roomList[i].left && !roomList[i].up && roomList[i].right && roomList[i].down)
            {
                room = Instantiate(GameObject.Find("RoomRightDown"), roomList[i].location, Quaternion.identity);
            }
            else if (roomList[i].left && !roomList[i].up && !roomList[i].right && !roomList[i].down)
            {
                room = Instantiate(GameObject.Find("RoomLeft"), roomList[i].location, Quaternion.identity);
            }
            else if (!roomList[i].left && !roomList[i].up && roomList[i].right && !roomList[i].down)
            {
                room = Instantiate(GameObject.Find("RoomRight"), roomList[i].location, Quaternion.identity);
            }
            else if (!roomList[i].left && roomList[i].up && !roomList[i].right && !roomList[i].down)
            {
                room = Instantiate(GameObject.Find("RoomUp"), roomList[i].location, Quaternion.identity);
            }
            else if (!roomList[i].left && !roomList[i].up && !roomList[i].right && roomList[i].down)
            {
                room = Instantiate(GameObject.Find("RoomDown"), roomList[i].location, Quaternion.identity);
            }
            rooms.Add(room);
        }

        GameObject.Find("Madeline").transform.position = new Vector2(-7, 0);

    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(roomList[i].location, Vector3.one * roomSize);
            Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            Gizmos.DrawCube(roomList[i].location, Vector3.one * roomSize);
        }
    }

    // Update is called once per frame
    void Update()
    {
        cd++;

        if (Input.GetKeyDown(KeyCode.Return) && cd>=300)
        {
            cd = 0;

            SetSeed();

            CreatePath();
        }
    }
    
}

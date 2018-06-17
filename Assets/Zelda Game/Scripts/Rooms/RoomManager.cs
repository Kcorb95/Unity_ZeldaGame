using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData
{
    public string Name; //Name of room
    public int X; // x and y coord of room
    public int Y;
}

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance; //Our singleton

    private string m_CurrentWorldName = "Overworld"; //Base part of the world name

    private RoomData m_CurrentLoadRoomData; //Currently loading room
    private Queue<RoomData> m_LoadRoomQueue = new Queue<RoomData>(); //Queue for the rooms to load
    private List<RoomParent> m_LoadedRooms = new List<RoomParent>(); //Our loaded rooms

    private bool m_IsLoadingRoom = false; //If the room is still loading

    private void Awake()
    {
        Instance = this; //part of singleton
    }

    private void Start()
    {
        //Make sure the start and end rooms are loaded no matter what
        LoadRoom("Start", 0, 0);
        LoadRoom("End", 1, -2);//Hardcoded is bad
    }

    private void Update()
    {
        UpdateRoomQueue();
    }

    private void UpdateRoomQueue()
    {
        if (m_IsLoadingRoom == true) return;

        if (m_LoadRoomQueue.Count == 0) return;

        m_CurrentLoadRoomData = m_LoadRoomQueue.Dequeue();
        m_IsLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(m_CurrentLoadRoomData));
    }

    private void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y) == true) return;

        RoomData newRoomData = new RoomData();
        newRoomData.Name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        m_LoadRoomQueue.Enqueue(newRoomData);
    }

    private IEnumerator LoadRoomRoutine(RoomData data)
    {
        string levelName = m_CurrentWorldName + data.Name;

        AsyncOperation loadLevel = Application.LoadLevelAdditiveAsync(levelName);

        while (loadLevel.isDone == false) yield return null;
    }

    public void RegisterRoom(RoomParent roomParent)
    {
        roomParent.transform.position = new Vector3(
            m_CurrentLoadRoomData.X * roomParent.Width,
            m_CurrentLoadRoomData.Y * roomParent.Height, 0);

        roomParent.X = m_CurrentLoadRoomData.X;
        roomParent.Y = m_CurrentLoadRoomData.Y;
        roomParent.name = m_CurrentWorldName + "-" + m_CurrentLoadRoomData.Name + " " + roomParent.X + ", " + roomParent.Y;
        roomParent.transform.parent = transform;

        m_IsLoadingRoom = false;

        if (m_LoadedRooms.Count == 0)
        {
            GameCamera.Instance.CurrentRoom = roomParent;
        }

        m_LoadedRooms.Add(roomParent);
    }

    private bool DoesRoomExist(int x, int y)
    {
        return m_LoadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    private string GetRandomRegularRoomName()
    {
        string[] possibleRooms = new string[] { //Room name
            "Empty",
            "Regular00",
            "Regular01",
            "Regular02",
            "Regular03",
            "Regular04",
            "Regular05",
            "Regular06",
            "Puzzle00",
            "Puzzle01",
            "Puzzle02"
        };

        return possibleRooms[Random.Range(0, possibleRooms.Length)]; //The total rooms
    }

    public void OnPlayerEnterRoom(RoomParent roomParent)
    {
        GameCamera.Instance.CurrentRoom = roomParent;

        //Our room buffer and allthat so no empty sides
        LoadRoom(GetRandomRegularRoomName(), roomParent.X + 1, roomParent.Y);
        LoadRoom(GetRandomRegularRoomName(), roomParent.X - 1, roomParent.Y);
        LoadRoom(GetRandomRegularRoomName(), roomParent.X, roomParent.Y + 1);
        LoadRoom(GetRandomRegularRoomName(), roomParent.X, roomParent.Y - 1);
    }
}
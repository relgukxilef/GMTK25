using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Game game;
    public bool frozen = false;

    public Dictionary<Vector2Int, Room> rooms;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        // Order between Awakes is not fixed, so everything needs to be set up
        // in common parent
        game = GetComponentInParent<Game>();
        rooms = new Dictionary<Vector2Int, Room>();
        foreach (var room in GetComponentsInChildren<Room>())
        {
            rooms.Add(room.Position, room);
            if (room.agents.Count > 0)
                continue;
            room.board = this;
            room.game = game;
            room.agents = new List<Agent>();
        }
        foreach (var agent in GetComponentsInChildren<Agent>())
        {
            agent.board = this;
            agent.game = game;
            agent.Room = Vector2Int.RoundToInt(agent.transform.localPosition);
        }
        foreach (var enemy in GetComponentsInChildren<Enemy>())
        {
            enemy.board = this;
            enemy.game = game;
            enemy.Room = Vector2Int.RoundToInt(enemy.transform.localPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Game game;
    public Board board;
    private Room room;

    public Vector2Int Room
    {
        get => Vector2Int.RoundToInt(room.transform.localPosition);
        set
        {
            if (room)
                room.enemies.Remove(this);
            room = board.rooms[value];
            if (!room.enemies.Contains(this))
                // Instantiate seems to copy some members and not others?
                room.enemies.Add(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Step()
    {
        Vector2Int[] directions = {
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
        };
        Array.Sort(directions, (a, b) => UnityEngine.Random.Range(-1, 1));

        foreach (var direction in directions)
        {
            if (!board.rooms.ContainsKey(Room + direction))
                continue;

            Room += direction;
            break;
        }
    }
}

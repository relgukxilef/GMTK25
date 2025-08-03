using System;
using UnityEngine;

[Serializable]
public struct ConsistentRandom
{
    public uint seed;
    public void See(uint value)
    {
        seed += value;
        seed *= 836973217;
    }
    public void See(int value)
    {
        See((uint)value);
    }
    public int Range(uint maxExclusive)
    {
        See(1);
        return (int)(((ulong)seed * maxExclusive) >> 32);
    }
    public int Range(int minInclusive, int maxExclusive)
    {
        return Range((uint)(maxExclusive - minInclusive)) + minInclusive;
    }
}

public class Enemy : MonoBehaviour
{
    public Game game;
    public Board board;
    public ConsistentRandom random;
    public bool captured;
    public SpriteRenderer spriteRenderer;
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
        if (captured)
            spriteRenderer.color -= new Color(1, 1, 1, Time.deltaTime);
    }

    public void Step()
    {
        if (captured)
            return;
        Vector2Int[] directions = {
            new(1, 0), new(-1, 0), new(0, 1), new(0, -1),
        };
        Array.Sort(directions, (a, b) => random.Range(-1, 1));
        
        if (room.agents.Count > 0)
        {
            game.capturableEnemies.Remove(this);
        }

        foreach (var direction in directions)
        {
            if (!board.rooms.ContainsKey(Room + direction))
                continue;

            Room += direction;
            break;
        }

        if (room.agents.Count > 0)
        {
            game.capturableEnemies.Add(this);
        }
    }
}

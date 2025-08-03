using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class Room : MonoBehaviour, IPointerClickHandler
{
    public Board board;
    public SpriteRenderer spriteRenderer;
    public bool valid = false;
    public Game game;

    public List<Agent> agents;
    public List<Enemy> enemies;

    public Vector2Int Position =>
        Vector2Int.RoundToInt(transform.localPosition);

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!valid)
            return;

        var agent = game.selection;
        var target = board;

        game.BeginMove(agent.board);

        // copy target to know timeline if it's in the past
        if (target.frozen)
        {
            var startLine = target.transform.position;
            target = Instantiate<Board>(target, game.transform);
            var position = target.transform.position;
            position.y = game.lineOffset * game.timelines;
            position.x += game.timeOffset;
            target.transform.position = position;
            game.timelines++;
            target.frozen = false;
            agent.timeTravelCharges--;
            var split = Instantiate(game.timeLineSplit, game.transform);
            split.height = (int)(position.y - startLine.y);
            split.width = game.timeOffset;
            split.transform.position = startLine;
        }
        else
        {
            game.transform.position -= new Vector3(game.timeOffset, 0, 0);
        }
        
        // move agent, maybe animated
        agent.board = target;
        agent.transform.parent = target.transform;
        agent.Room = Position;
        game.selection = null;

        var enemies = new List<Enemy>(target.rooms[Position].enemies);
        foreach (var enemy in enemies)
        {
            enemy.Step();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        valid = false;
        var agent = game.selection;
        if (agent)
        {
            var offset = Position - agent.Room;
            if (
                agent.board == board &&
                Math.Abs(offset.x) + Math.Abs(offset.y) == 1
            )
            {
                valid = true;
            }
            // TODO: don't allow traveling into the future or across timelines
            // Or would traveling into the future be cool?
            if (
                agent.timeTravelCharges > 0 &&
                offset.y == 0 &&
                offset.x == 0
            )
            {
                valid = true;
            }
        }

        spriteRenderer.enabled = valid;

        for (int i = 0; i < agents.Count; i++)
        {
            Assert.AreEqual(agents[i].board, board);
            agents[i].transform.localPosition =
                transform.localPosition +
                new Vector3(-0.5f + (i + 1f) /
                (agents.Count + enemies.Count + 1), 0);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            Assert.AreEqual(enemies[i].board, board);
            enemies[i].transform.localPosition =
                transform.localPosition +
                new Vector3(-0.5f + (agents.Count + i + 1f) /
                (agents.Count + enemies.Count + 1), 0);
        }
    }
}

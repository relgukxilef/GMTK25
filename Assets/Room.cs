using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

class Utility
{
    public static Vector3 MoveTowardsIn(Vector3 current, Vector3 target, float secondsLeft)
    {
        if (secondsLeft <= Time.deltaTime)
            return target;
        var distance = Vector3.Distance(current, target);
        return Vector3.MoveTowards(current, target, distance / secondsLeft * Time.deltaTime);
    }
}

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
            var startLine = target.transform.localPosition;
            target = Instantiate<Board>(target, game.transform);
            var position = target.transform.localPosition;
            position.y = game.lineOffset * game.timelines;
            position.x += game.timeOffset;
            target.transform.localPosition = position;
            game.timelines++;
            target.frozen = false;
            agent.timeTravelCharges--;
            var split = Instantiate(game.timeLineSplit, game.transform);
            split.height = (int)(position.y - startLine.y);
            split.width = game.timeOffset;
            split.transform.localPosition = startLine;
        }
        else
        {
            game.transform.localPosition -= new Vector3(game.timeOffset, 0, 0);
        }

        // move agent, maybe animated
        agent.board = target;
        agent.transform.parent = target.transform;
        agent.Room = Position;
        game.selection = null;

        // TODO: This appears to only move the current timeline
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
        {
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
        }

        spriteRenderer.enabled = valid;

        /*
        Instantiate copies fields, so the agents/enemies arrays in room point
        to the original agents/enemies instead of the ones belonging to the copy
        of room. So Awake has to fix the arrays. But the order of objects in
        Awake is undefined, so agents would randomly swap places when the board
        is copied if we run this code for frozen boards. 
        TODO: this also freezes agents mid-animation D:
        */
        if (board.frozen)
            return;

        for (int i = 0; i < agents.Count; i++)
        {
            var agent = agents[i];
            Assert.AreEqual(agent.board, board);
            var destination = transform.localPosition +
                new Vector3(-0.5f + (i + 1f) /
                (agents.Count + enemies.Count + 1), 0);
            agent.transform.localPosition = Utility.MoveTowardsIn(
                agent.transform.localPosition,
                destination, 1f - game.secondsSinceTurn * 1f
            );
        }

        if (game.secondsSinceTurn < 1)
            return;

        for (int i = 0; i < enemies.Count; i++)
        {
            var enemy = enemies[i];
            Assert.AreEqual(enemy.board, board);
            var destination =
                transform.localPosition +
                new Vector3(-0.5f + (agents.Count + i + 1f) /
                (agents.Count + enemies.Count + 1), 0);
            enemy.transform.localPosition = Utility.MoveTowardsIn(
                enemy.transform.localPosition,
                destination, 2f - game.secondsSinceTurn * 1f
            );
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Room : MonoBehaviour, IPointerClickHandler
{
    public Board board;
    public SpriteRenderer spriteRenderer;
    public bool valid = false;
    private Game game;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!valid)
            return;

        var agent = game.selection;
        var source = agent.board;
        var target = board;

        // copy source
        Instantiate<Board>(source).frozen = true;
        // move source to next time step, maybe animated
        source.transform.position += new Vector3(game.timeOffset, 0, 0);
        // copy target if it's frozen
        if (target.frozen)
        {
            target = Instantiate<Board>(target);
            var position = target.transform.position;
            position.y = game.lineOffset * game.timelines;
            target.transform.position = position;
            game.timelines++;
            target.frozen = false;
        }
        // move agent, maybe animated
        agent.transform.parent = target.transform;
        agent.transform.localPosition = transform.localPosition;
        agent.board = target;
        game.selection = null;
        game.camera.transform.position = target.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        game = board.game;
    }

    // Update is called once per frame
    void Update()
    {
        valid = false;
        if (game.selection)
        {
            Vector2 offset =
                transform.position -
                game.selection.gameObject.transform.position;
            if (Math.Abs(offset.x) + Math.Abs(offset.y) == 1)
            {
                valid = true;
            }
            if (
                game.selection.timeTravelCharges > 0 &&
                offset.y == 0 &&
                offset.x % game.timeOffset == 0 &&
                offset.x < 0
            )
            {
                valid = true;
            }
        }

        spriteRenderer.enabled = valid;
    }
}

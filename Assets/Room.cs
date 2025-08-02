using System;
using Unity.VisualScripting;
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
        var oldPosition = game.selection.transform.position;
        game.selection.transform.position = transform.position;
        var newBoard = Instantiate(board);
        var offset = new Vector3(6, 0, 0);
        newBoard.transform.position += offset;
        game.selection.transform.position = oldPosition;
        game.selection = null;
        board.frozen = true;
        game.camera.transform.position += offset;
    }

    // Start is called before the first frame update
    void Start()
    {
        game = board.game;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: fog of war
        valid = false;
        if (game.selection)
        {
            Vector2 offset =
                game.selection.gameObject.transform.position -
                transform.position;
            if (Math.Abs(offset.x) + Math.Abs(offset.y) == 1)
            {
                valid = true;
            }
        }

        spriteRenderer.enabled = valid;
    }
}

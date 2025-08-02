using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Room : MonoBehaviour, IPointerClickHandler
{
    public Game game;
    public SpriteRenderer spriteRenderer;
    public bool valid = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!valid)
            return;
        game.selection.transform.position = transform.position;
        game.selection = null;
        // TODO: instead of moving the piece, clone the board and move the piece in the clone
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

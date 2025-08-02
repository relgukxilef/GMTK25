using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Agent : MonoBehaviour, IPointerClickHandler
{
    public Board board;
    private Game game;
    public int timeTravelCharges;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (board.frozen)
            return;
        if (game.selection)
        {
            game.selection = null;
            return;
        }

        game.selection = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        game = board.game;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

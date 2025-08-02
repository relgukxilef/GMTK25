using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Agent : MonoBehaviour, IPointerClickHandler
{
    public Game game;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Hi");
        if (game.selection)
            return;

        game.selection = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

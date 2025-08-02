using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public float enemyTurnSpeed = 0.5f;

    public Agent selection;
    public bool playerTurn = true;
    public float enemyTurnTime = 0.0f;
    public new GameObject camera;
    public int timeOffset, lineOffset;
    public int timelines = 1;
    public GameObject timeLineSprite;
    public TimeLineSplit timeLineSplit;

    // TODO: Allow zooming and panning of camera

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureButton : MonoBehaviour
{
    public Game game;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool active = game.capturableEnemies.Count > 0;
        spriteRenderer.enabled = active;
    }
}
